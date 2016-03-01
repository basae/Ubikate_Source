using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ubikate.Core
{
    public class AuthenticatedContext : IApplicationContext
    {
        public ApiUser CurrentUser { get; set; }
        public bool Unrestricted { get; set; }
    }
}