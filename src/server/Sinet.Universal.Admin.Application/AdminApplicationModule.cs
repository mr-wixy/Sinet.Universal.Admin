using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Sinet.Universal.Admin;

[DependsOn(
    typeof(AdminDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(AdminApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AdminApplicationModule : AbpModule
{

    //public override void PreConfigureServices(ServiceConfigurationContext context)
    //{
    //    PreConfigure<IdentityBuilder>(identityBuilder =>
    //    {
    //        identityBuilder.AddSignInManager<AbpSignInManager>()
    //            .AddUserValidator<AbpIdentityUserValidator>(); ;
    //    });
    //    base.PreConfigureServices(context);
    //}

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdminApplicationModule>();
        });
    }
}
