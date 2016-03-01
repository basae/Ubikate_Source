using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ubikate.Core
{
    public interface IApplicationContext
    {
        ApiUser CurrentUser { get; set; }
        bool Unrestricted { get; set; }
    }
}