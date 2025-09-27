using biz.ateb.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.ateb.Repository.Authentication
{
    public interface IAuthenticationRepository : IGenericRepository<biz.ateb.Entities.Usuario>
    {
        bool VerifyPassword(string hash, string password);
    }
}
