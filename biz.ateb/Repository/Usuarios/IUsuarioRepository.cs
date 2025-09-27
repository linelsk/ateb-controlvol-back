using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Usuarios
{
    public interface IUsuarioRepository : IGenericRepository<biz.ateb.Entities.Usuario>
    {
        List<Usuario> getUsers();
        string HashPassword(string password);
    }
}
