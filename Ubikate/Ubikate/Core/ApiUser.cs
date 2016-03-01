using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Ubikate.Core
{
    public class ApiUser : IPrincipal
    {
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity { get; private set; }

        public User User { get { return Identity as User; } }

        public ApiUser(IIdentity identity)
        {
            Identity = identity;
        }
    }
}