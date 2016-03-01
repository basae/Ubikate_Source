using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Ubikate.Core
{
    public class BaseController : ApiController, IContextAware
    {
        public IApplicationContext Context { get; private set; }

        public BaseController()
        {
            Context = new AuthenticatedContext { Unrestricted = false };
        }

        protected string getCustomHeaderValue(string headerName)
        {
            IEnumerable<string> customHeader;
            string headerValue;

            if (!Request.Headers.TryGetValues(headerName, out customHeader))
            {
                headerValue = string.Empty;
            }
            else
            {
                headerValue = customHeader.FirstOrDefault();
            }

            return headerValue;
        }
    }
}