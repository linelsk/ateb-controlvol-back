using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.UsuariosPlantas
{
    public interface IUsuarioPlantaRepository : IGenericRepository<biz.ateb.Entities.UsuarioPlantum>
    {
        List<UsuarioPlantum> GetPlantasByUsuario(string usuarioID);
        void DeleteAllPlantasByUsuario(string usuarioID);
        Boolean SaveAllPlantasBUsuario(List<UsuarioPlantum> listaPlantas);
        List<string> ListaPlantasByUsuario(string usuarioID);
    }
}
