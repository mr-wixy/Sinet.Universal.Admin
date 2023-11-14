using Masa.Blazor.Presets;
using Masa.Blazor;
using Microsoft.Extensions.DependencyInjection;
using Sinet.Universal.Admin.Client.Services;
using Sinet.Universal.Admin.RCL;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using System.Reflection;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Volo.Abp.Http.Client;
using Sinet.Universal.Admin.RCL.Global;

namespace Sinet.Universal.Admin.Client
{
    [DependsOn(
        typeof(AdminHttpApiClientModule),
        typeof(AdminBlazorServerModule),
        typeof(AbpAutofacModule))]
    public class ClientModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientActions.Add((remoteServiceName, context, client) =>
                {
                    //var appService = context.GetRequiredService<IAppService>();
                    //var accessToken = appService.AccessToken;
                    if (!string.IsNullOrEmpty(GlobalVariableData.AccessToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariableData.AccessToken);
                    }
                });
            });
        }


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = context.Services.GetConfiguration();
            context.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(config["RemoteServices:Default:BaseUrl"]) });

            context.Services.AddSingleton<MainWindow>();
            context.Services.AddMasaBlazor(options =>
            {
                options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
                {
                    {
                        PopupComponents.SNACKBAR, new Dictionary<string, object?>()
                        {
                            { nameof(PEnqueuedSnackbars.Closeable), true },
                            { nameof(PEnqueuedSnackbars.Position), SnackPosition.TopCenter }
                        }
                    }
                };
            });

            var basePath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(AdminBlazorServerModule)).Location) ?? throw new Exception("Get the assembly root directory exception!");
            context.Services.AddNav(Path.Combine(basePath, $"wwwroot/nav/nav.json"));

            context.Services.AddWpfBlazorWebView();
#if DEBUG
            context.Services.AddBlazorWebViewDeveloperTools();
#endif
            context.Services.AddScoped<IAppService, ClientAppService>();
        }
    }
}
