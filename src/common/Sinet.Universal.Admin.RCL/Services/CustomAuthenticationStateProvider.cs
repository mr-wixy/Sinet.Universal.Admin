using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sinet.Universal.Admin.RCL;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    //private readonly IStorageService _storageService;

    //public CustomAuthenticationStateProvider(IStorageService storageService)
    //{
    //    _storageService = storageService;

    //    // 用于监听Token变化从而更新界面变化
    //    _storageService.TokenChange = () =>
    //    {
    //        // 如果token是空的则情况消息返回到登录界面 如果不为空则登录成功
    //        if (string.IsNullOrEmpty(_storageService.Token))
    //        {
    //            User = null;

    //            var identity = new ClaimsIdentity();
    //            User = new ClaimsPrincipal(identity);

    //            NotifyAuthenticationStateChanged(
    //                Task.FromResult(new AuthenticationState(User)));
    //        }
    //        else
    //        {

    //            // 解密token
    //            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_storageService.Token);
    //            var claims = jwt.Claims;
    //            AuthenticateUser(claims);
    //        }
    //    };
    //}

    private AuthenticationState authenticationState;

    public CustomAuthenticationStateProvider(AuthenticationService service)
    {
        authenticationState = new AuthenticationState(service.CurrentUser);

        service.UserChanged += (newUser) =>
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(newUser)));
        };
    }

    private ClaimsPrincipal? User { get; set; }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (User != null)
        {
            return Task.FromResult(new AuthenticationState(User));
        }

        var identity = new ClaimsIdentity();
        User = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(User));
    }

    public void AuthenticateUser(IEnumerable<Claim> _claims)
    {
        var identity = new ClaimsIdentity(_claims, "iCTC");

        User = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(User)));
    }

    public void Logout()
    {
        User = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(User)));
    }
}