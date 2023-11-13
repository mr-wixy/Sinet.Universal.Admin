using Sinet.Universal.Admin.RCL.Pages.App.User;
using System.Security.Claims;
using Volo.Abp.Users;
using static Volo.Abp.Identity.IdentityPermissions;

namespace Sinet.Universal.Admin.RCL.Services
{
    public class BaseAppService : IAppService
    {
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public BaseAppService(CustomAuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public void AuthenticateUser(IEnumerable<Claim> claims)
        {
            _authenticationStateProvider.AuthenticateUser(claims);
        }

        public void ReAuthenticate(ICurrentUser user)
        {
            if(user != null && user.IsAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name )
                };
                AuthenticateUser(claims);
            }
            
        }
    }
}
