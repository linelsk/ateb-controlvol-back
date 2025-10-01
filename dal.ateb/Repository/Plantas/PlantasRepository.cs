using biz.ateb.Entities;
using biz.ateb.Repository.Planta;
using biz.ateb.Repository.Generic;
using dal.ateb.DBContext;
using dal.ateb.Repository.EmpresasPlantas;
using dal.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.Plantas
{
    public class PlantasRepository : GenericRepository<biz.ateb.Entities.Plantum>, IPlantaRepository
    {
        public PlantasRepository(controlvolContext context) : base(context)
        {
        }
        public List<Plantum> GetPlantas()
        {
            var usersList = _context.Planta.Select(u => u).ToList();

            return usersList;
        }
    }
}
