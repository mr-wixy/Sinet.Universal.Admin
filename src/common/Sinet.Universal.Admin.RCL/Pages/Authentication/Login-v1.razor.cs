using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Volo.Abp.Identity;

namespace Sinet.Universal.Admin.RCL.Pages.Authentication
{
    public partial class Login_v1
    {
        [Inject]
        [NotNull]
        public IUserAccountAppService? UserAccountAppService { get; set; }
        
        [Inject]
        [NotNull]
        public IAppService? _appService { get; set; }

        [Inject]
        [NotNull]
        public IPopupService? PopupService { get; set; }

        public string UserName { get; set; } = "admin";

        public string Password { get; set; } = "1q2w3E*";

        public async Task LoginTo()
        {
            try
            {
                var res = await UserAccountAppService.LoginByPass(UserName, Password);
                if(res == "Succeeded")
                {
                    await _appService.LoginToServer(UserName, Password);
                    await PopupService.EnqueueSnackbarAsync("登录成功，正在跳转!", AlertTypes.Success);
                }
                else
                    await PopupService.EnqueueSnackbarAsync("登录失败！", AlertTypes.Error);

            }
            catch (Exception ex)
            {
                await PopupService.EnqueueSnackbarAsync(ex);
            }
        }
    }
}
