using api.ateb.Models.ApiResponse;
using api.ateb.Models.Auth;
using AutoMapper;
using biz.ateb.Entities;
using biz.ateb.Repository.Usuarios;
using biz.ateb.Repository.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using api.flexiform.rarp.Models.Usuarios;

namespace api.ateb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationRepository _authenticationRepository; 

        public AuthController(IConfiguration configuration, IMapper mapper, IUsuarioRepository usuarioRepository, IAuthenticationRepository authenticationRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = new ApiResponse<object>();

            try
            {
                var user = _usuarioRepository.Find(c => c.Correo == request.Username);
                if (user != null)
                {
                    if (_authenticationRepository.VerifyPassword(user.Password, request.Password))
                    {
                        var userData = _mapper.Map<UsuarioDto>(user);
                        var token = GenerateJwtToken(request.Username);
                        response.Result = new {
                            token,
                            usuario = userData
                        };
                        response.Success = true;
                        response.Message = "success";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Usuario y/o contraseña incorrecta";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario y/o contraseña incorrecta";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}