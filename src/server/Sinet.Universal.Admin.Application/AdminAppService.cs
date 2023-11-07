using System;
using System.Collections.Generic;
using System.Text;
using Sinet.Universal.Admin.Localization;
using Volo.Abp.Application.Services;

namespace Sinet.Universal.Admin;

/* Inherit your application services from this class.
 */
public abstract class AdminAppService : ApplicationService
{
    protected AdminAppService()
    {
        LocalizationResource = typeof(AdminResource);
    }
}
