using biz.ateb.Entities;
using biz.ateb.Repository.Empresa;
using biz.ateb.Repository.Generic;
using dal.ateb.DBContext;
using dal.ateb.Repository.EmpresasPlantasRepository;
using dal.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.Empresas
{
    public class EmpresasRepository : GenericRepository<biz.ateb.Entities.Empresa>, IEmpresaRepository
    {
        public EmpresasRepository(controlvolContext context) : base(context)
        {
        }
        public List<Empresa> GetEmpresas()
        {
            var usersList = _context.Empresas.Select(u => u).ToList();

            return usersList;
        }
    }
}
