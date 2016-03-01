
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using VModels;
using Business;

namespace Ubikate.Core
{
    public enum AuthScheme
    {
        Token
    }
    public class TokenAuthenticationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple { get; private set; }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!ShouldAuthenticate(actionContext))
            {
                return await continuation();
            }

            //if (!AuthorizationComplete(actionContext.Request.Headers.Authorization))
            //{
            //    return await EndUnauthorized(requestAuth: true);
            //}

            if (!actionContext.Request.Headers.Contains("Authorization"))
            {
                return await EndUnauthorized(requestAuth: true);
            }
            // ReSharper disable PossibleNullReferenceException
            var controller = actionContext.ControllerContext.Controller as IContextAware;

            if (controller == null)
            {
                return await EndUnauthorized(requestAuth: false);
            }

            string host = actionContext.Request.Headers.Host;

            // for requests coming in as localhost or the internal api url, allow them as unrestricted
            if ((host == "localhost"
                || host == "127.0.0.1"
                )
                && actionContext.ActionDescriptor.GetCustomAttributes<UnrestrictedRequestOnlyAttribute>().Any())
            {
                controller.Context.Unrestricted = true;
                return await continuation();
            }

            var usr = await AuthenticateRequest(actionContext.Request);
            if (usr != null)
            {
                controller.Context.CurrentUser = new ApiUser(usr);
                // ReSharper restore PossibleNullReferenceException
                return await continuation();
            }

            return await EndUnauthorized(requestAuth: false);
        }

        private static Task<HttpResponseMessage> EndUnauthorized(bool requestAuth)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            if (requestAuth)
            {
                response.Headers.Add("WWW-Authenticate", "Token realm=\"System API\"");
            }

            return Task.FromResult(response);
        }

        private static async Task<User> AuthenticateRequest(HttpRequestMessage request)
        {
            //Debug.Assert(request.Headers.Authorization.Scheme == AuthScheme.Token.ToString());

            User usr;
            try
            {

                var authToken = request.Headers.GetValues("Authorization").FirstOrDefault();
                TokenBusiness _repository = new TokenBusiness();
                var currentUser = await _repository.getSubscriberByToken(authToken);
                usr = new User(currentUser.Result.FirstOrDefault().userId, currentUser.Result.FirstOrDefault().realName, "User", true);
            }
            catch
            {
                usr = null;
            }
            return usr;
        }

        private bool ShouldAuthenticate(HttpActionContext actionContext)
        {
            return (actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AuthenticateAttribute>()
                                 .Any()
                    ||
                    actionContext.ActionDescriptor.GetCustomAttributes<AuthenticateAttribute>().Any()
                    &&
                    actionContext.ControllerContext.Controller is IContextAware);
        }

        private bool AuthorizationComplete(AuthenticationHeaderValue authorization)
        {
            return authorization != null
                   && authorization.Scheme == AuthScheme.Token.ToString();
        }
    }
}