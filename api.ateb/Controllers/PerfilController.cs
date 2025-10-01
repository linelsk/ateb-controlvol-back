using api.ateb.Models.Acciones;
using api.ateb.Models.ApiResponse;
using api.ateb.Models.Empresas;
using api.ateb.Models.Perfiles;
using AutoMapper;
using biz.ateb.Entities;
using biz.ateb.Repository.Acciones;
using biz.ateb.Repository.Empresa;
using biz.ateb.Repository.Perfiles;
using biz.ateb.Repository.Proveedor;
using dal.ateb.Repository.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace api.ateb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PerfilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IPerfilEmpresaRepository _perfilEmpresaRepository;
        private readonly IAccionRepository _accionRepository;
        private readonly IPerfilAccionesRepository _perfilAccionRepository;
        public PerfilController(IMapper mapper, IPerfilRepository perfilRepository, IPerfilEmpresaRepository perfilEmpresaRepository, IAccionRepository accionRepository, IPerfilAccionesRepository perfilAccionesRepository)
        {
            _mapper = mapper;
            _perfilRepository = perfilRepository;
            _perfilEmpresaRepository = perfilEmpresaRepository;
            _accionRepository = accionRepository;
            _perfilAccionRepository = perfilAccionesRepository;
        }

        [HttpPost("CrearPerfil", Name = "CrearPerfil")]
        public ActionResult<ApiResponse<CrearPerfilDto>> CrearPerfil(CrearPerfilDto item)
        {
            var response = new ApiResponse<CrearPerfilDto>();

            try
            {
                if (_perfilRepository.Exists(x => x.PerfilId == item.PerfilId))
                {
                    response.Success = false;
                    response.Message = "Ya existe un perfil con el mismo ID.";
                    response.Result = null;
                    return StatusCode(201, response);
                }

                var perfil = _mapper.Map<Perfile>(item);
                var clienteCreado = _perfilRepository.Add(perfil);
                response.Message = "Perfil creado correctamente";
                response.Result = _mapper.Map<CrearPerfilDto>(clienteCreado);
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
        [HttpPost("ActualizarPerfil", Name = "ActualizarPerfil")]
        public ActionResult<ApiResponse<CrearPerfilDto>> ActualizarPerfil(CrearPerfilDto item)
        {
            var response = new ApiResponse<CrearPerfilDto>();

            try
            {
                if (!_perfilRepository.Exists(x => x.PerfilId == item.PerfilId))
                {
                    response.Success = false;
                    response.Message = "No existe el perfil que intentas actualizar";
                    response.Result = null;
                    return StatusCode(201, response);
                }

                var perfil = _mapper.Map<Perfile>(item);

                var clienteCreado = _perfilRepository.Update(perfil, perfil.PerfilId);
                response.Message = "PErfil actualizado correctamente";
                response.Result = _mapper.Map<CrearPerfilDto>(clienteCreado);
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

        [HttpGet("GetAllPerfiles", Name = "GetAllPerfiles")]
        public ActionResult<ApiResponse<List<ListaPerfilesDto>>> GetAllPerfiles()
        {
            var response = new ApiResponse<List<ListaPerfilesDto>>();

            try
            {
                response.Result = _mapper.Map<List<ListaPerfilesDto>>(_perfilRepository.GetPerfiles());
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
        [HttpGet("GetPerfilEmpresas", Name = "GetPerfilEmpresas")]
        public ActionResult<ApiResponse<List<PerfilEmpresaDto>>> GetPerfilEmpresas(long perfilId)
        {
            var response = new ApiResponse<List<PerfilEmpresaDto>>();

            try
            {

                var data = _perfilEmpresaRepository.GetPerfilEmpresaByPerfil(perfilId);
                response.Result = data.Select(x => new PerfilEmpresaDto
                {
                    EmpresaId = x.EmpresaId,
                    DescripcionEmpresa = x.Empresa.RazonSocial,
                    PerfilId = perfilId
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

        [HttpPost("GuardaRelacionPerfilEmpresa", Name = "GuardaRelacionPerfilEmpresa")]
        public ActionResult<Boolean> GuardaRelacionPerfilEmpresa(List<CrearPerfilEmpresaDto> item)
        {
            var response = new ApiResponse<Boolean>();

            try
            {
                if (item.Count > 0)
                {
                    _perfilEmpresaRepository.DeleteAllEmpresaByPerfil(item[0].PerfilId);
                    response.Result = _perfilEmpresaRepository.SaveAllEmpresaByPerfil(_mapper.Map<List<PerfilEmpresa>>(item));
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
        [HttpGet("GetAcciones", Name = "GetAcciones")]
        public ActionResult<ApiResponse<List<ListaAccionDto>>> GetAcciones()
        {
            var response = new ApiResponse<List<ListaAccionDto>>();

            try
            {
                response.Result = _mapper.Map<List<ListaAccionDto>>(_accionRepository.GetAllAcciones());
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
        [HttpGet("GetPerfilAccionesByPerfil", Name = "GetPerfilAccionesByPerfil")]
        public ActionResult<ApiResponse<List<PerfilAccionDto>>> GetPerfilAccionesByPerfil(long perfilId)
        {
            var response = new ApiResponse<List<PerfilAccionDto>>();

            try
            {

                var data = _perfilAccionRepository.GetPerfilAccionesByPerfil(perfilId);
                response.Result = data.Select(x => new PerfilAccionDto
                {
                    CodAccion= x.CodAccion,
                    DescripcionAccion = x.CodAccionNavigation.Descripcion,
                    PerfilId = perfilId
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

        [HttpPost("GuardaRelacionPerfilAccion", Name = "GuardaRelacionPerfilAccion")]
        public ActionResult<Boolean> GuardaRelacionPerfilAccion(List<CrearPerfilAccionDto> item)
        {
            var response = new ApiResponse<Boolean>();

            try
            {
                if (item.Count > 0)
                {
                    _perfilAccionRepository.DeleteAllAccionesByPerfil(item[0].PerfilId);
                    response.Result = _perfilAccionRepository.SaveAllAccionesByPerfil(_mapper.Map<List<PerfilAccion>>(item));
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
