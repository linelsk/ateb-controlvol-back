using biz.ateb.Entities;
using dal.ateb.DBContext;
using biz.ateb.Repository.Proveedor;
using dal.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.ateb.Repository.Acciones;

namespace dal.ateb.Repository.Acciones
{
    public class AccionesRepository : GenericRepository<biz.ateb.Entities.Accione>, IAccionRepository
    {
        public AccionesRepository(controlvolContext context) : base(context)
        {
        }
        public List<Accione> GetAllAcciones()
        {
            var accionesList = _context.Acciones.Select(u => u).ToList();

            return accionesList;
        }
    }
}
