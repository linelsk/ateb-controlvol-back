using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Empresa
{
    public interface IEmpresaPlantaRepository : IGenericRepository<biz.ateb.Entities.EmpresaPlantum>
    {
            List<EmpresaPlantum> GetEmpresaPlantasByEmpresa(string empresaId);
            void DeleteAllPlantasByEmpresa(string empresaId);
            Boolean SaveAllPlantasByEmpresa(List<EmpresaPlantum> listaPlantas);
    }
}
