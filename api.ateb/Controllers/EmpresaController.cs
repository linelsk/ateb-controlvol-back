using api.ateb.Models.ApiResponse;
using api.ateb.Models.Empresas;
using api.ateb.Models.Plantas;
using api.ateb.Models.Proveedores;
using api.ateb.Models.Usuarios;
using api.flexiform.rarp.Models.Usuarios;
using AutoMapper;
using biz.ateb.Entities;
using biz.ateb.Repository.Empresa;
using biz.ateb.Repository.Planta;
using biz.ateb.Repository.Proveedor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.ateb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmpresaPlantaRepository _empresaPlantaRepository;
        private readonly IPlantaRepository _plantaRepository;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IEmpresaProveedorRepository _empresaProveedorRepository;

        public EmpresaController(IMapper mapper, IEmpresaRepository empresaRepository, IEmpresaPlantaRepository empresaPlantaRepository, IPlantaRepository plantaRepository, IProveedorRepository proveedorRepository,
                                 IEmpresaProveedorRepository empresaProveedorRepository)
        {
            _mapper = mapper;
            _empresaRepository = empresaRepository;
            _empresaPlantaRepository = empresaPlantaRepository;
            _plantaRepository = plantaRepository;
             _proveedorRepository = proveedorRepository;
            _empresaProveedorRepository = empresaProveedorRepository;
        }

        [HttpGet("GetAllEmpresas", Name = "GetAllEmpresas")]
        public ActionResult<ApiResponse<List<ListaEmpresasDto>>> GetAllEmpresas()
        {
            var response = new ApiResponse<List<ListaEmpresasDto>>();

            try
            {
                response.Result = _mapper.Map<List<ListaEmpresasDto>>(_empresaRepository.GetEmpresas());
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


        [HttpGet("GetEmpresaPlanta", Name = "GetEmpresaPlanta")]
        public ActionResult<ApiResponse<List<EmpresaPlantaDto>>> GetEmpresaPlanta(string empresa)
        {
            var response = new ApiResponse<List<EmpresaPlantaDto>>();

            try
            {

                var data = _empresaPlantaRepository.GetEmpresaPlantasByEmpresa(empresa);
                response.Result = data.Select(x => new EmpresaPlantaDto
                {
                    empresaId = x.EmpresaId,
                    razonSocial = x.Empresa.RazonSocial,
                    plantaId = x.PlantaId,
                    nombrePlanta = x.Planta.DescripcionPlanta
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

        [HttpGet("GetAllPlantas", Name = "GetAllPlantas")]
        public ActionResult<ApiResponse<List<ListaPlantaDto>>> GetAllPlantas()
        {
            var response = new ApiResponse<List<ListaPlantaDto>>();

            try
            {

                response.Result = _mapper.Map<List<ListaPlantaDto>>(_plantaRepository.GetPlantas());
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

        [HttpPost("GuardaRelacionEmpresaPlanta", Name = "GuardaRelacionEmpresaPlanta")]
        public ActionResult<Boolean> GuardaRelacionEmpresaPlanta(List<CrearEmpresaPlantaDto> item)
        {
            var response = new ApiResponse<Boolean>();

            try
            {
                if (item.Count > 0)
                {
                    _empresaPlantaRepository.DeleteAllPlantasByEmpresa(item[0].empresaId);
                    response.Result = _empresaPlantaRepository.SaveAllPlantasByEmpresa(_mapper.Map<List<EmpresaPlantum>>(item));
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
        
        [HttpGet("GetAllProveedores", Name = "GetAllProveedores")]
        public ActionResult<ApiResponse<List<ListaProveedoresDto>>> GetAllProveedores()
        {
            var response = new ApiResponse<List<ListaProveedoresDto>>();

            try
            {
                response.Result = _mapper.Map<List<ListaProveedoresDto>>(_proveedorRepository.GetAllProveedores());
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
        [HttpGet("GetEmpresaProveedores", Name = "GetEmpresaProveedores")]
        public ActionResult<ApiResponse<List<EmpresaProveedorDto>>> GetEmpresaProveedores(string empresa)
        {
            var response = new ApiResponse<List<EmpresaProveedorDto>>();

            try
            {

                var data = _empresaProveedorRepository.GetEmpresaProveedorByEmpresa(empresa);
                response.Result = data.Select(x => new EmpresaProveedorDto
                {
                    empresaId = x.EmpresaId,
                    razonSocial = x.Empresa.RazonSocial,
                    proveedorId = x.ProveedorId,
                    nombreProveedor = x.Proveedor.RazonSocialP
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

        [HttpPost("GuardaRelacionEmpresaProveedor", Name = "GuardaRelacionEmpresaProveedor")]
        public ActionResult<Boolean> GuardaRelacionEmpresaProveedor(List<CrearEmpresaProveedorDto> item)
        {
            var response = new ApiResponse<Boolean>();

            try
            {
                if (item.Count > 0)
                {
                    _empresaProveedorRepository.DeleteAlProveedoresByEmpresa(item[0].EmpresaId);
                    response.Result = _empresaProveedorRepository.SaveAllProveedoresByEmpresa(_mapper.Map<List<EmpresaProveedor>>(item));
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
