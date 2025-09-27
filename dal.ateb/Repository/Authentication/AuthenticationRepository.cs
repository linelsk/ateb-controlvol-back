using biz.ateb.Repository.Authentication;
using dal.ateb.DBContext;
using dal.ateb.Repository.Generic;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace dal.ateb.Repository.Authentication
{
    public class AuthenticationRepository : GenericRepository<biz.ateb.Entities.Usuario>, IAuthenticationRepository
    {
        public AuthenticationRepository(controlvolContext context) : base(context)
        {

        }

        public bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }
    }
}
