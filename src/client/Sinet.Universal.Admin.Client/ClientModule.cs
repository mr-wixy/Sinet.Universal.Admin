using Masa.Blazor.Presets;
using Masa.Blazor;
using Microsoft.Extensions.DependencyInjection;
using Sinet.Universal.Admin.Client.Services;
using Sinet.Universal.Admin.RCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

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
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
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

            context.Services.AddWpfBlazorWebView();
#if DEBUG
            context.Services.AddBlazorWebViewDeveloperTools();
#endif
            context.Services.AddScoped<IAppService, ClientAppService>();
        }
    }
}
