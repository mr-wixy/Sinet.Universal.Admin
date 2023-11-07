using Sinet.Universal.Admin.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sinet.Universal.Admin;

[DependsOn(
    typeof(AdminEntityFrameworkCoreTestModule)
    )]
public class AdminDomainTestModule : AbpModule
{

}
