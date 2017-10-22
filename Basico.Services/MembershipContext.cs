using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basico.Entities;
using System.Security.Principal;

namespace Basico.Services
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public identityUser User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
