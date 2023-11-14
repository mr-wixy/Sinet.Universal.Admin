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
        public string AccessToken { get; set; }

        Task AuthenticateUserAsync(ICurrentUser user);

        void AuthenticateUser(IEnumerable<Claim> claims);

        Task LoginToServer(string username, string password);

        Task LogOut();
    }
}
