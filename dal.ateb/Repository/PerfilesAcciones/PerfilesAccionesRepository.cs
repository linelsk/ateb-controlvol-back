using biz.ateb.Entities;
using biz.ateb.Repository.Perfiles;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.PerfilesAcciones
{
    public class PerfilesAccionesRepository : GenericRepository<biz.ateb.Entities.PerfilAccion>, IPerfilAccionesRepository
    {
        public PerfilesAccionesRepository(controlvolContext context) : base(context)
        {
        }
        public List<PerfilAccion> GetPerfilAccionesByPerfil(long perfilId)
        {
            var plantaEmpresaLista = _context.PerfilAccions
                                .Where(x => x.PerfilId == perfilId)
                                .Include(x => x.CodAccionNavigation) 
                                .ToList();
            return plantaEmpresaLista;
        }
        public void DeleteAllAccionesByPerfil(long perfilId)
        {
            var accionesToDelete = _context.PerfilAccions.Where(x => x.PerfilId == perfilId).ToList();
            if (accionesToDelete.Any())
            {
                _context.PerfilAccions.RemoveRange(accionesToDelete);
                _context.SaveChanges();
            }
        }
        public Boolean SaveAllAccionesByPerfil(List<PerfilAccion> listaAcciones)
        {
            try
            {
                _context.PerfilAccions.AddRange(listaAcciones);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
