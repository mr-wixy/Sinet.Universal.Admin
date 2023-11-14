using Microsoft.AspNetCore.Components;
using Sinet.Universal.Admin.RCL;
using Sinet.Universal.Admin.RCL.Global;
using Sinet.Universal.Admin.RCL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using Volo.Abp.Users;

namespace Sinet.Universal.Admin.Client.Services
{
    public class ClientAppService : BaseAppService
    {
        private readonly HttpClient _httpClient; 
        private readonly CookieStorage _cookieStorage; 

        public ClientAppService(CustomAuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager
            , CookieStorage cookieStorage
            , HttpClient httpClient
            ) : base(authenticationStateProvider, navigationManager)
        {
            _httpClient = httpClient;
            _cookieStorage = cookieStorage;
        }

        public override async Task AuthenticateUserAsync(ICurrentUser user)
        {
            GlobalVariableData.AccessToken = await _cookieStorage.GetAsync("token");
            if(!string.IsNullOrEmpty(GlobalVariableData.AccessToken))
            {
                var tokenInfo = new JwtSecurityToken(GlobalVariableData.AccessToken);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, tokenInfo.Payload.FirstOrDefault(p => p.Key == "sub").Value.ToString()),
                    new Claim(ClaimTypes.Name, tokenInfo.Payload.FirstOrDefault(p => p.Key == "unique_name").Value.ToString() )
                };
                AuthenticateUser(claims);
            }
        }

        public override async Task LoginToServer(string username, string password)
        {
            var getTokenContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"client_id", "Admin_App"},
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                {"scope", "Admin profile openid email roles offline_access"},
            });

            var res = _httpClient.PostAsync("connect/token", getTokenContent).Result;
            if (res.IsSuccessStatusCode)
            {
                var resObj = res.Content.ReadAsStringAsync().Result;
                var token = JsonSerializer.Deserialize<TokenInfo>(resObj);
                _cookieStorage.SetAsync("token", token.AccessToken);
                _navigationManager.NavigateTo("/", true);
            }
            else if (res.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var resObj = res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                var resObj = res.Content.ReadAsStringAsync().Result?.ToString();
            }
        }

        public override Task LogOut()
        {
            _cookieStorage.SetAsync("token", string.Empty);
            return base.LogOut();
        }

    }
}
