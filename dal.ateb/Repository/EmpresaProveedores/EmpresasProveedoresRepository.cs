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

namespace dal.ateb.Repository.EmpresasProveedoresRepository
{
    public class EmpresasProveedoresRepository : GenericRepository<biz.ateb.Entities.EmpresaProveedor>, IEmpresaProveedorRepository
    {
        public EmpresasProveedoresRepository(controlvolContext context) : base(context)
        {
        }
        public List<EmpresaProveedor> GetEmpresaProveedorByEmpresa(string empresaId)
        {
            var proveedorEmpresaLista = _context.EmpresaProveedors
                                .Where(x => x.EmpresaId == empresaId)
                                .Include(x => x.Empresa) 
                                .Include(x => x.Proveedor)
                                .ToList();
            return proveedorEmpresaLista;
        }
        public void DeleteAlProveedoresByEmpresa(string empresaId)
        {
            var proveedorToDelete = _context.EmpresaProveedors.Where(x => x.EmpresaId == empresaId).ToList();
            if (proveedorToDelete.Any())
            {
                _context.EmpresaProveedors.RemoveRange(proveedorToDelete);
                _context.SaveChanges();
            }
        }
        public Boolean SaveAllProveedoresByEmpresa(List<EmpresaProveedor> listaProveedores)
        {
            try
            {
                _context.EmpresaProveedors.AddRange(listaProveedores);
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
