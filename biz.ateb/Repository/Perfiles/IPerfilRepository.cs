using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Perfiles
{
    public interface IPerfilRepository : IGenericRepository<biz.ateb.Entities.Perfile>
    {
        List<biz.ateb.Entities.Perfile> GetPerfiles();
    }
}
