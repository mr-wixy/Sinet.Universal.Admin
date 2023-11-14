using Microsoft.AspNetCore.Components;
using Sinet.Universal.Admin.RCL;
using Sinet.Universal.Admin.RCL.Services;
using System.Threading.Tasks;

namespace Sinet.Universal.Admin.Server.Host.Services
{
    public class ServerHostAppService : BaseAppService
    {
        public ServerHostAppService(
            CustomAuthenticationStateProvider authenticationStateProvider
            , NavigationManager navigationManager) : base(authenticationStateProvider, navigationManager)
        {
        }

        public override Task LoginToServer(string username, string password)
        {
            _navigationManager.NavigateTo($"/app/redirect_login?name={username}&pass={password}", true);
            return Task.CompletedTask;
        }

        public override Task LogOut()
        {
            _navigationManager.NavigateTo($"/app/logout", true);
            return Task.CompletedTask;
        }
    }
}
