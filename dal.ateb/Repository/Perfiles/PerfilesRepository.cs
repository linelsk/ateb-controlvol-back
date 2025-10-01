using biz.ateb.Entities;
using biz.ateb.Repository.Perfiles;
using biz.ateb.Repository.Generic;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.Perfiles
{
    public class PerfilesRepository : GenericRepository<biz.ateb.Entities.Perfile>, IPerfilRepository
    {
        public PerfilesRepository(controlvolContext context) : base(context)
        {
        }
        public List<Perfile> GetPerfiles()
        {
            var perfilList = _context.Perfiles.Select(u => u).ToList();

            return perfilList;
        }
    }
}
