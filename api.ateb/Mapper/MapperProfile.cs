using api.ateb.Models.Acciones;
using api.ateb.Models.Empresas;
using api.ateb.Models.Perfiles;
using api.ateb.Models.Plantas;
using api.ateb.Models.Proveedores;
using api.ateb.Models.Usuarios;
using api.flexiform.rarp.Models.Usuarios;
using AutoMapper;
using biz.ateb.Entities;


namespace api.main.tecnicah.Mapper
{
    /// <summary>
    /// Mapper de Models a Entities
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MapperProfile()
        {

            #region Usuarios
            CreateMap<Usuario, UsuarioDto>().ReverseMap(); 
            CreateMap<HistoryPass, HistoryPassDto>().ReverseMap();
            CreateMap<Perfile, PerfileDto>().ReverseMap();
            CreateMap<CrearUsuarioDto, Usuario>().ReverseMap();
            CreateMap<CrearUsuarioPlantaDto, UsuarioPlantum>().ReverseMap();
            CreateMap<UsuarioPlantum, CrearUsuarioPlantaDto>().ReverseMap();
            CreateMap<Usuario, GetUsuarioDto>().ReverseMap();

            #endregion
            #region Empresas
            CreateMap<Empresa, ListaEmpresasDto>().ReverseMap();
            CreateMap<Plantum, ListaPlantaDto>().ReverseMap();
            CreateMap<CrearEmpresaPlantaDto, EmpresaPlantum>().ReverseMap();
            CreateMap<ListaProveedoresDto, Proveedore>().ReverseMap();
            CreateMap<CrearEmpresaProveedorDto, EmpresaProveedor>().ReverseMap();
            CreateMap<CrearEmpresaDto, Empresa>().ReverseMap();
            CreateMap<Empresa, CrearEmpresaDto>().ReverseMap();

            #endregion

            #region Perfiles
            CreateMap<ListaPerfilesDto, Perfile>().ReverseMap();
            CreateMap<PerfilEmpresa, CrearPerfilEmpresaDto>().ReverseMap();
            CreateMap<CrearPerfilDto, Perfile>().ReverseMap();
            CreateMap<ListaAccionDto, Accione>().ReverseMap();
            CreateMap<CrearPerfilAccionDto, PerfilAccion>().ReverseMap();
            #endregion

            

        }
    }
}
