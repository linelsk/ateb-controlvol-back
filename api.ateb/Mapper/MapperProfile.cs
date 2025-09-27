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
            //CreateMap<Usuario, UsuarioDto>()
            //    .ForMember(dest => dest.IdTipoUsuarioNavigation, opt => opt.MapFrom(src => src.IdTipoUsuarioNavigation))
            //    .ReverseMap();
            #endregion
        }
    }
}
