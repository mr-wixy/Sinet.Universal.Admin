using Sinet.Universal.Admin.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Sinet.Universal.Admin;

public abstract class AdminComponentBase : AbpComponentBase
{
    protected AdminComponentBase()
    {
        LocalizationResource = typeof(AdminResource);
    }
}
