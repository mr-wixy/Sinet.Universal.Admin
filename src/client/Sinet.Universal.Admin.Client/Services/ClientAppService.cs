using Sinet.Universal.Admin.RCL;
using Sinet.Universal.Admin.RCL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinet.Universal.Admin.Client.Services
{
    public class ClientAppService : BaseAppService
    {
        public ClientAppService(CustomAuthenticationStateProvider authenticationStateProvider) : base(authenticationStateProvider)
        {
        }
    }
}
