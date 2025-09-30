using biz.ateb.Entities;
using biz.ateb.Repository.Empresa;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.EmpresasPlantasRepository
{
    public class EmpresasPlantasRepository : GenericRepository<biz.ateb.Entities.EmpresaPlantum>, IEmpresaPlantaRepository
    {
        public EmpresasPlantasRepository(controlvolContext context) : base(context)
        {
        }
        public List<EmpresaPlantum> GetEmpresaPlantasByEmpresa(string empresaId)
        {
            var plantaEmpresaLista = _context.EmpresaPlanta
                                .Where(x => x.EmpresaId == empresaId)
                                .Include(x => x.Empresa) 
                                .Include(x => x.Planta)
                                .ToList();
            return plantaEmpresaLista;
        }
        public void DeleteAllPlantasByEmpresa(string empresaId)
        {
            var plantasToDelete = _context.EmpresaPlanta.Where(x => x.EmpresaId == empresaId).ToList();
            if (plantasToDelete.Any())
            {
                _context.EmpresaPlanta.RemoveRange(plantasToDelete);
                _context.SaveChanges();
            }
        }
        public Boolean SaveAllPlantasByEmpresa(List<EmpresaPlantum> listaPlantas)
        {
            try
            {
                _context.EmpresaPlanta.AddRange(listaPlantas);
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
