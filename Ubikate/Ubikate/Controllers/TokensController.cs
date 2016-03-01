using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Business;
using VModels;

namespace Ubikate.Controllers
{
    public class TokensController : ApiController
    {
        private TokenBusiness _tokenRepository;
        public TokensController()
        {
            _tokenRepository = new TokenBusiness();
        }
        // GET api/tokens
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tokens/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tokens
        public async Task<UserResponse> Post([FromBody]VMlogin user)
        {

            ResponseService<UserResponse> isUser = await _tokenRepository.getTokenForUser(user);

            if (isUser.Error)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,isUser.Message));
            }
            return isUser.Result.FirstOrDefault();
        }

        // PUT api/tokens/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tokens/5
        public void Delete(int id)
        {
        }
    }
}
