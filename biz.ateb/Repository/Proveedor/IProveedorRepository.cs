using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Proveedor
{
    public interface IProveedorRepository : IGenericRepository<biz.ateb.Entities.Proveedore>
    {
        List<biz.ateb.Entities.Proveedore> GetAllProveedores();
    }
}
