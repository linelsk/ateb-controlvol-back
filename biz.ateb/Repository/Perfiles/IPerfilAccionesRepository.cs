using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Perfiles
{
    public interface IPerfilAccionesRepository : IGenericRepository<PerfilAccion>
    {
        List<PerfilAccion> GetPerfilAccionesByPerfil(long perfilId);
        void DeleteAllAccionesByPerfil(long perfilId);
        bool SaveAllAccionesByPerfil(List<PerfilAccion> listaAcciones);
    }
}
