using biz.ateb.Entities;
using biz.ateb.Repository.Proveedor;
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

namespace dal.ateb.Repository.Proveedores
{
    public class ProveedoresRepository : GenericRepository<biz.ateb.Entities.Proveedore>, IProveedorRepository
    {
        public ProveedoresRepository(controlvolContext context) : base(context)
        {
        }
        public List<Proveedore> GetAllProveedores()
        {
            var usersList = _context.Proveedores.Select(u => u).ToList();

            return usersList;
        }
    }
}
