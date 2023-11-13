using System.Security.Claims;
using Volo.Abp.Identity;

namespace Sinet.Universal.Admin.RCL.Pages.Authentication
{
    public partial class Login_v1
    {
        [Inject]
        public IUserAccountAppService UserAccountAppService { get; set; }
        
        [Inject]
        public IAppService _appService { get; set; }

        [Inject]
        public IIdentityUserAppService UserAppService { get; set; }

        [Inject]
        public IPopupService PopupService { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public async Task LoginTo()
        {
            try
            {
                //var users = await UserAppService.GetListAsync(new GetIdentityUsersInput());
                var res = await UserAccountAppService.LoginByPass(UserName, Password);

                if(res == "Succeeded")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, UserName),
                        new Claim(ClaimTypes.Name, UserName )
                    };
                    //_appService.AuthenticateUser(claims);
                    await PopupService.EnqueueSnackbarAsync("登录成功，正在跳转!", AlertTypes.Success);

                    await Task.Delay(1000);
                    Nav.NavigateTo($"/app/redirect_login?name={UserName}&pass={Password}", true);
                }
                else
                    await PopupService.EnqueueSnackbarAsync("登录失败！", AlertTypes.Error);

            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync($"登录失败: {ex.Message}", AlertTypes.Error);
            }
        }
    }
}
