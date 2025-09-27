using biz.ateb.Entities;
using biz.ateb.Repository.Usuarios;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.flexform.rarp.Repository.Usuarios
{
    public class UsuarioRepository : GenericRepository<biz.ateb.Entities.Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(controlvolContext context) : base(context)
        {
        }
        public List<Usuario> getUsers()
        {
            var usersList = _context.Usuarios.Select(u => u).ToList();

            return usersList;
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public override biz.ateb.Entities.Usuario Add(biz.ateb.Entities.Usuario user)
        {
            user.Password = HashPassword(user.Password);
            return base.Add(user);
        }
    }
}
