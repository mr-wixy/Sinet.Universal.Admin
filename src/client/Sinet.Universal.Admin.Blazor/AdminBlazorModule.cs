﻿using System;
using System.Net.Http;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sinet.Universal.Admin.Blazor.Menus;
using OpenIddict.Abstractions;
using Volo.Abp.AspNetCore.Components.WebAssembly.LeptonXLiteTheme;
using Volo.Abp.AspNetCore.Components.Web.LeptonXLiteTheme.Themes.LeptonXLite;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.SettingManagement.Blazor.WebAssembly;
using Volo.Abp.TenantManagement.Blazor.WebAssembly;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.IO;
using System.Collections.Generic;

namespace Sinet.Universal.Admin.Blazor;

[DependsOn(
    typeof(AbpAutofacWebAssemblyModule),
    typeof(AdminHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyLeptonXLiteThemeModule),
    typeof(AbpIdentityBlazorWebAssemblyModule),
    typeof(AbpTenantManagementBlazorWebAssemblyModule),
    typeof(AbpSettingManagementBlazorWebAssemblyModule)
)]
public class AdminBlazorModule : AbpModule
{
    //public override void ConfigureServices(ServiceConfigurationContext context)
    //{
    //    var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
    //    var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        

    //    ConfigureAuthentication(builder);
    //    ConfigureHttpClient(context, environment);
    //    ConfigureBlazorise(context);
    //    ConfigureRouter(context);
    //    ConfigureUI(builder);
    //    ConfigureMenu(context);
    //    ConfigureAutoMapper(context);
    //}

    public override async Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        //using var httpclient = new HttpClient();
        //var navList = await httpclient.GetFromJsonAsync<List<NavModel>>(Path.Combine(builder.HostEnvironment.BaseAddress, $"nav/nav.json")) ?? throw new Exception("please configure the Navigation!");
        //builder.Services.AddNav(navList); 
        
        //await builder.Services
        //     .AddMasaBlazor(builder =>
        //     {
        //         builder.ConfigureTheme(theme =>
        //         {
        //             theme.Themes.Light.Primary = "#4318FF";
        //             theme.Themes.Light.Accent = "#4318FF";
        //         });
        //     })
        //     .AddI18nForWasmAsync(Path.Combine(builder.HostEnvironment.BaseAddress, "i18n"));



        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureUI(builder);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);

        await base.ConfigureServicesAsync(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(AdminBlazorModule).Assembly;
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new AdminMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.UserOptions.NameClaim = OpenIddictConstants.Claims.Name;
            options.UserOptions.RoleClaim = OpenIddictConstants.Claims.Role;

            options.ProviderOptions.DefaultScopes.Add("Admin");
            options.ProviderOptions.DefaultScopes.Add("roles");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("phone");
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#ApplicationContainer");

    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminBlazorModule>();
        });
    }
}
