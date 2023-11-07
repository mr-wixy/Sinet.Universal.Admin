using Volo.Abp.Modularity;

namespace Sinet.Universal.Admin;

[DependsOn(
    typeof(AdminApplicationModule),
    typeof(AdminDomainTestModule)
    )]
public class AdminApplicationTestModule : AbpModule
{

}
