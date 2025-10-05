using biz.ateb.Entities;
using biz.ateb.Repository.Empresa;
using biz.ateb.Repository.UsuariosPlantas;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.ateb.Repository.UsuariosPlantas
{
    public class UsuarioPlantaRepository : GenericRepository<biz.ateb.Entities.UsuarioPlantum>, IUsuarioPlantaRepository
    {
        public UsuarioPlantaRepository(controlvolContext context) : base(context)
        {
        }
        public List<UsuarioPlantum> GetPlantasByUsuario(string usuarioID)
        {
            var plantaEmpresaLista = _context.UsuarioPlanta
                                .Where(x => x.UsuarioId == usuarioID)
                                .Include(x => x.Planta)
                                .ToList();
            return plantaEmpresaLista;
        }

        public List<string> ListaPlantasByUsuario(string usuarioID)
        {
            var plantaEmpresaLista = _context.UsuarioPlanta
                                .Where(x => x.UsuarioId == usuarioID)
                                .Select(x => x.PlantaId)
                                .ToList();
            return plantaEmpresaLista;
        }
        public void DeleteAllPlantasByUsuario(string usuarioID)
        {
            var plantasToDelete = _context.UsuarioPlanta.Where(x => x.UsuarioId == usuarioID).ToList();
            if (plantasToDelete.Any())
            {
                _context.UsuarioPlanta.RemoveRange(plantasToDelete);
                _context.SaveChanges();
            }
        }
        public Boolean SaveAllPlantasBUsuario(List<UsuarioPlantum> listaPlantas)
        {
            try
            {
                _context.UsuarioPlanta.AddRange(listaPlantas);
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
