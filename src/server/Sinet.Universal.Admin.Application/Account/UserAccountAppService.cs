using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using Volo.Abp.Account.Settings;
using Volo.Abp.Settings;
using Volo.Abp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Sinet.Universal.Admin
{
    public class UserAccountAppService : ApplicationService, IUserAccountAppService
    {
        private readonly ILogger<ServerInfoAppService> _logger;
        
        protected AbpSignInManager SignInManager { get; }
        protected IdentityUserManager UserManager { get; }
        protected UserManager<Volo.Abp.Identity.IdentityUser> _userManager { get; }
        protected ISettingProvider SettingProvider { get; }
        protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }


        public UserAccountAppService(
            ILogger<ServerInfoAppService> logger
            , AbpSignInManager signInManager
            , UserManager<Volo.Abp.Identity.IdentityUser> userManager
            )
        {
            _logger = logger;
            SignInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<string> LoginByPass(string name, string pass, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("LoginByPass");

            var user = await _userManager.FindByNameAsync(name);
            if(user == null)
            {
                return null;
            }

            var result = await SignInManager.CheckPasswordSignInAsync(user, pass,  false);

            if(result.Succeeded)
                return result.ToString();

            return null;

        }

        [HttpGet("app/redirect_login")]
        public async Task<IActionResult> RedirectLogin(string name, string pass, string redirect = null, CancellationToken cancellationToken = default)
        {
            var result = await SignInManager.PasswordSignInAsync(name, pass, false, false);

            if (result.Succeeded)
            {
                return new RedirectResult(redirect??"/");
            }

            return null;
        }


        [HttpGet("app/logout")]
        public async Task<IActionResult> SignOut(string redirect = null, CancellationToken cancellationToken = default)
        {
            await SignInManager.SignOutAsync();
            return new RedirectResult(redirect ?? "/");
        }
    }
}
