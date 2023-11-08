using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Sinet.Universal.Admin.RCL
{
    [DependsOn(
        typeof(AdminApplicationContractsModule)
       )]
    public class AdminBlazorServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            context.Services.AddScoped<CookieStorage>();
            context.Services.AddScoped<GlobalConfig>();

            context.Services.AddScoped<AuthenticationService>();
            context.Services.AddScoped<CustomAuthenticationStateProvider>();
            context.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());
        }
    }
}
