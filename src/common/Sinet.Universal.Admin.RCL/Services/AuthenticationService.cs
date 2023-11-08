
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sinet.Universal.Admin.RCL
{
    public class AuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? currentUser;

        public ClaimsPrincipal CurrentUser
        {
            get { return currentUser ?? new(); }
            set
            {
                currentUser = value;

                if (UserChanged is not null)
                {
                    UserChanged(currentUser);
                }
            }
        }
    }
}
