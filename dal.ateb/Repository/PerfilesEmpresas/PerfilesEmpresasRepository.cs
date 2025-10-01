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

namespace dal.ateb.Repository.PerfilesEmpresas
{
    public class PerfilesEmpresasRepository : GenericRepository<biz.ateb.Entities.PerfilEmpresa>, IPerfilEmpresaRepository
    {
        public PerfilesEmpresasRepository(controlvolContext context) : base(context)
        {
        }
        public List<PerfilEmpresa> GetPerfilEmpresaByPerfil(long perfilId)
        {
            var plantaEmpresaLista = _context.PerfilEmpresas
                                .Where(x => x.PerfilId == perfilId)
                                .Include(x => x.Empresa) 
                                .ToList();
            return plantaEmpresaLista;
        }
        public void DeleteAllEmpresaByPerfil(long perfilId)
        {
            var plantasToDelete = _context.PerfilEmpresas.Where(x => x.PerfilId == perfilId).ToList();
            if (plantasToDelete.Any())
            {
                _context.PerfilEmpresas.RemoveRange(plantasToDelete);
                _context.SaveChanges();
            }
        }
        public Boolean SaveAllEmpresaByPerfil(List<PerfilEmpresa> listaEmpresas)
        {
            try
            {
                _context.PerfilEmpresas.AddRange(listaEmpresas);
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
