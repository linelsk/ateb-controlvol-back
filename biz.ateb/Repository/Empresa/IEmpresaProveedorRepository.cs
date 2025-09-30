using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Empresa
{
    public interface IEmpresaProveedorRepository : IGenericRepository<biz.ateb.Entities.EmpresaProveedor>
    {
        List<EmpresaProveedor> GetEmpresaProveedorByEmpresa(string empresaId);
        void DeleteAlProveedoresByEmpresa(string empresaId);
        Boolean SaveAllProveedoresByEmpresa(List<EmpresaProveedor> listaProveedores);
    }
}
