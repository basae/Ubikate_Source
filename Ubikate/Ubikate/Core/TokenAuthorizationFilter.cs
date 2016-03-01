using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Business;
using VModels;


namespace Ubikate.Core
{
    public class TokenAuthorizationFilter : IActionFilter
    {
        public async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ShouldAuthenticate(filterContext)) return;
            var controller = filterContext.Controller as IContextAware;
            if (controller == null)
            {
                throw new Exception("Filter could not get a valid controller");
            }
            var usr = await AuthenticateRequest(filterContext.HttpContext.Request);
            if (usr != null)
            {
                controller.Context.CurrentUser = new ApiUser(usr);
            }

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        private async Task<User> AuthenticateRequest(HttpRequestBase Request)
        {
            var HeaderRequest = Request.Headers["Authorization"];
            string authToken;
            if (string.IsNullOrEmpty(HeaderRequest))
            {
                HeaderRequest = Request.Params["Token"];
                if (string.IsNullOrEmpty(HeaderRequest))
                {
                    throw new ArgumentException("Request can't be authenticated - no authorization header or token parameter");

                }
                else
                    authToken = authToken = HeaderRequest.Trim().Replace(" ", "+");
            }
            else
            {
                var authParams = HeaderRequest.Trim().Split(' ');
                if (authParams.Length < 2)
                {
                    throw new ArgumentException("Request can't be authenticated - no token");
                }

                if (!authParams[0].Equals("Token", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ArgumentException("Request can't be authenticated - invalid auth scheme");
                }
                authToken = authParams[1];
            }
            User urs=null;
            try
            {
                TokenBusiness _respository=new TokenBusiness();
                ResponseService<VMlogin> currentUser = await _respository.getSubscriberByToken(authToken);
                if (!currentUser.Error)
                {
                    urs = new User(currentUser.Result.FirstOrDefault().userId, currentUser.Result.FirstOrDefault().realName, "User", true);
                }
            }
            catch
            {
                urs = null;
            }
            return urs;
        }

        private bool ShouldAuthenticate(ActionExecutingContext actionContext)
        {
            var authenticate = (actionContext
                .ActionDescriptor
                .ControllerDescriptor
                .IsDefined(typeof(AuthenticateAttribute), false)
                ||
                actionContext.ActionDescriptor.IsDefined(typeof(AuthenticateAttribute), false)
                &&
                actionContext
                .ActionDescriptor
                .ControllerDescriptor
                .ControllerType
                .GetInterfaces().Contains(typeof(IContextAware)));

            return authenticate && ConfigurationManager.AppSettings["Maps.Controller.Authenticate"] == "Yes";
        }

    }
}