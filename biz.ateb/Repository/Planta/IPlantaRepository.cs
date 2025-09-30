using biz.ateb.Entities;
using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Planta
{
    public interface IPlantaRepository : IGenericRepository<biz.ateb.Entities.Plantum>
    {
        List<biz.ateb.Entities.Plantum> GetPlantas();
    }
}
