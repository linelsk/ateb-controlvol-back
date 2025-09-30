using api.ateb.Models.Empresas;
using api.ateb.Models.Plantas;
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
            CreateMap<Empresa, ListaEmpresasDto>().ReverseMap();
            CreateMap<Plantum, ListaPlantaDto>().ReverseMap();
            CreateMap<CrearEmpresaPlantaDto, EmpresaPlantum>().ReverseMap();
            #endregion
        }
    }
}
