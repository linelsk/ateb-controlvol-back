using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Acciones
{
    public interface IAccionRepository : IGenericRepository<Entities.Accione>
    {
        List<Entities.Accione> GetAllAcciones();
    }
}
