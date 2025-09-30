using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Empresa
{
    public interface IEmpresaRepository : IGenericRepository<biz.ateb.Entities.Empresa>
    {
        List<biz.ateb.Entities.Empresa> GetEmpresas();
    }
}
