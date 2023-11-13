using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Sinet.Universal.Admin.RCL
{
    public interface IAppService
    {
        void ReAuthenticate(ICurrentUser user);

        void AuthenticateUser(IEnumerable<Claim> claims);
    }
}
