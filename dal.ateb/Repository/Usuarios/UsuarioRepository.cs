using biz.ateb.Entities;
using biz.ateb.Repository.Usuarios;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using Microsoft.EntityFrameworkCore;
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
            var usersList = _context.Usuarios
                                    .Include(x => x.Perfil)
                                    .Select(u => u).ToList();

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
        public biz.ateb.Entities.Usuario Actualiza(biz.ateb.Entities.Usuario user)
        {
            var consulta = _context.Usuarios.Where(x => x.UsuarioId == user.UsuarioId).Select(u => u).FirstOrDefault();

            if (consulta != null)
            {
                if (user.Password != null && user.Password != "")
                {
                    consulta.Password = HashPassword(consulta.Password);
                    consulta.UpdatePassword = DateOnly.FromDateTime(DateTime.Now);
                }

                consulta.Activo = user.Activo;
                consulta.Nombre = user.Nombre;
                consulta.PerfilId = user.PerfilId;
                _context.SaveChanges();
                return consulta;
            }
            else
            {
                return null;
            }

        }
        public biz.ateb.Entities.Usuario ActualizaPassword(biz.ateb.Entities.Usuario user)
        {
            var consulta = _context.Usuarios.Where(x => x.UsuarioId == user.UsuarioId).Select(u => u).FirstOrDefault();

            if (consulta != null)
            {
                if (user.Password != null && user.Password != "")
                {
                    consulta.Password = HashPassword(consulta.Password);
                    consulta.UpdatePassword = DateOnly.FromDateTime(DateTime.Now);
                }
                _context.SaveChanges();
                return consulta;
            }
            else
            {
                return null;
            }

        }
    }
}
