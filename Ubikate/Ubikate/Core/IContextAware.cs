using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ubikate.Core
{
    public interface IContextAware
    {
        IApplicationContext Context { get; }
    }
}