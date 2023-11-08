using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Sinet.Universal.Admin.RCL;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Sinet.Universal.Admin.Localization;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp;

namespace Sinet.Universal.Admin.Web;

[DependsOn(
    typeof(AdminHttpApiClientModule),
    typeof(AdminBlazorServerModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreComponentsServerBasicThemeModule)
   )]
public class AdminBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(AdminResource),
                typeof(AdminDomainSharedModule).Assembly,
                typeof(AdminBlazorServerModule).Assembly,
                typeof(AdminApplicationContractsModule).Assembly,
                typeof(AdminBlazorModule).Assembly
            );
        });

        //PreConfigure<OpenIddictBuilder>(builder =>
        //{
        //    builder.AddValidation(options =>
        //    {
        //        options.AddAudiences("Admin");
        //        options.UseLocalServer();
        //        options.UseAspNetCore();
        //    });
        //});
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddMasaBlazor(builder =>
        {
            builder.ConfigureTheme(theme =>
            {
                theme.Themes.Light.Primary = "#4318FF";
                theme.Themes.Light.Accent = "#4318FF";
            });
        }).AddI18nForServer("wwwroot/i18n");

        var basePath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(AdminBlazorServerModule)).Location) ?? throw new Exception("Get the assembly root directory exception!");
        context.Services.AddNav(Path.Combine(basePath, $"wwwroot/nav/nav.json"));

        //ConfigureAuthentication(context);
        ConfigureAutoMapper();
        ConfigureRouter(context);
    }

    //private void ConfigureAuthentication(ServiceConfigurationContext context)
    //{
    //    context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    //}

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(AdminBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminBlazorModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseConfiguredEndpoints();
    }
}
