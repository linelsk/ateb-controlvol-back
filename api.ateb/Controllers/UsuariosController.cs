using api.ateb.Models.ApiResponse;
using api.ateb.Models.Perfiles;
using api.ateb.Models.Usuarios;
using api.flexiform.rarp.Models.Usuarios;
using AutoMapper;
using biz.ateb.Entities;
using biz.ateb.Repository.Usuarios;
using biz.ateb.Repository.UsuariosPlantas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.ateb.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioPlantaRepository _usuarioPlantaRepository;

        public UsuariosController(IMapper mapper, IUsuarioRepository usuarioRepository, IUsuarioPlantaRepository usuarioPlantaRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _usuarioPlantaRepository = usuarioPlantaRepository;
        }

        
        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public ActionResult<ApiResponse<List<UsuarioDto>>> GetAllUsers()
        {
            var response = new ApiResponse<List<UsuarioDto>>();

            try
            {
                response.Result = _mapper.Map<List<UsuarioDto>>(_usuarioRepository.GetAllIncluding(x => x.HistoryPasses));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("GetUserByEmail", Name = "GetUserByEmail")]
        public ActionResult<ApiResponse<List<UsuarioDto>>> GetUserByEmail(string correo)
        {
            var response = new ApiResponse<List<UsuarioDto>>();

            try
            {
                response.Result = _mapper.Map<List<UsuarioDto>>(_usuarioRepository.Find(x => x.Correo == correo));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("GetUserByID", Name = "GetUserByID")]
        public ActionResult<ApiResponse<List<GetUsuarioDto>>> GetUserByID(string usuarioID)
        {
            var response = new ApiResponse<List<GetUsuarioDto>>();

            try
            {
                response.Result = _mapper.Map<List<GetUsuarioDto>>(_usuarioRepository.Find(x => x.UsuarioId == usuarioID));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("CrearUsuario", Name = "CrearUsuario")]
        public ActionResult<ApiResponse<CrearUsuarioDto>> CrearUsuario(CrearUsuarioDto item)
        {
            var response = new ApiResponse<CrearUsuarioDto>();

            try
            {
                if (_usuarioRepository.Exists(x => x.Correo == item.Correo))
                {
                    response.Success = false;
                    response.Message = "Ya existe un cliente con el mismo email.";
                    response.Result = null;
                    return StatusCode(201, response);
                }

                var usuario = _mapper.Map<Usuario>(item);
                usuario.PrimeraVez = true;
                usuario.UpdatePassword = DateOnly.FromDateTime(DateTime.Now);

                var clienteCreado = _usuarioRepository.Add(usuario);
                response.Message = "Usuario creado correctamente";
                response.Result = _mapper.Map<CrearUsuarioDto>(clienteCreado);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpPut("ActualizarUsuario", Name = "ActualizarUsuario")]
        public ActionResult<ApiResponse<CrearUsuarioDto>> ActualizarUsuario(CrearUsuarioDto item)
        {
            var response = new ApiResponse<CrearUsuarioDto>();

            try
            {

                var usuario = _mapper.Map<Usuario>(item);
                usuario.UpdatePassword = DateOnly.FromDateTime(DateTime.Now);
                var usuarioActualizado = _usuarioRepository.Update(usuario, item.UsuarioId);
                response.Result = _mapper.Map<CrearUsuarioDto>(usuarioActualizado);

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpDelete("EliminarCliente", Name = "EliminarCliente")]
        public ActionResult<ApiResponse<CrearUsuarioDto>> DeleteClient(string id)
        {
            var response = new ApiResponse<CrearUsuarioDto>();

            try
            {
                var usuario = _usuarioRepository.Find(x => x.UsuarioId == id);
                _usuarioRepository.Delete(usuario);
                response.Result = null;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpGet("GetPlantasByUsuario", Name = "GetPlantasByUsuario")]
        public ActionResult<ApiResponse<List<CrearUsuarioPlantaDto>>> GetPlantasByUsuario(string usuarioID)
        {
            var response = new ApiResponse<List<CrearUsuarioPlantaDto>>();

            try
            {

                var data = _usuarioPlantaRepository.GetPlantasByUsuario(usuarioID);
                response.Result = data.Select(x => new CrearUsuarioPlantaDto
                {
                    PlantaDescripcion = x.Planta.DescripcionPlanta,
                    UsuarioId = x.UsuarioId,
                    PlantaId = x.PlantaId
                }).ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("GuardaRelacionUsuarioPlanta", Name = "GuardaRelacionUsuarioPlanta")]
        public ActionResult<Boolean> GuardaRelacionUsuarioPlanta(List<CrearUsuarioPlantaDto> item)
        {
            var response = new ApiResponse<Boolean>();

            try
            {
                if (item.Count > 0)
                {
                    _usuarioPlantaRepository.DeleteAllPlantasByUsuario(item[0].UsuarioId);
                    response.Result = _usuarioPlantaRepository.SaveAllPlantasBUsuario(_mapper.Map<List<UsuarioPlantum>>(item));
                    response.Message = "Se guardaron correctamente los datos";
                }
                else
                {
                    response.Result = true;
                    response.Message = "Sin datos guardados";
                }


            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }
    }
}
