using System.Security.Claims;
using Volo.Abp.Users;

namespace Sinet.Universal.Admin.RCL.Services
{
    public class BaseAppService : IAppService
    {
        protected readonly NavigationManager _navigationManager;

        protected readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public string AccessToken { get; set; }

        public BaseAppService(CustomAuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public virtual async Task LoginToServer(string username, string password)
        {

        }

        public void AuthenticateUser(IEnumerable<Claim> claims)
        {
            _authenticationStateProvider.AuthenticateUser(claims);
        }

        public virtual Task AuthenticateUserAsync(ICurrentUser user)
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
            return Task.CompletedTask;
            
        }

        public virtual Task LogOut()
        {
            _authenticationStateProvider.Logout();
            return Task.CompletedTask;
        }
    }
}
