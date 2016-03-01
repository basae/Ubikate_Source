using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ubikate.Core;
using VModels;
using Business;
using System.Threading.Tasks;

namespace Ubikate.Controllers
{
    public class ProprietaryController : BaseController
    {
        ProprietaryBusiness _proprietary;

        public ProprietaryController()
        {
            _proprietary = new ProprietaryBusiness();
        }

        // GET api/proprietary
        [Authenticate]
        public async Task<IEnumerable<VMproprietary>> Get()
        {
            ResponseService<VMproprietary> response = await _proprietary.Get(null);
            if (response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Message));
            return response.Result;
        }

        // GET api/proprietary/5
        public async Task<VMproprietary> Get(int id)
        {
            ResponseService<VMproprietary> response = await _proprietary.Get(id);
            if (response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Message));
            return response.Result.FirstOrDefault();
        }

        // POST api/proprietary
        [Authenticate]
        public async Task<long> Post([FromBody]VMproprietary propietary)
        {
            ResponseService<long> Response =await _proprietary.Save(propietary,Context.CurrentUser.User.Id);
            if(Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.Result.FirstOrDefault();
        }

        // PUT api/proprietary/5
        [Authenticate]
        public async Task<bool> Put([FromBody]VMproprietary propietary)
        {
            ResponseService<long> Response = await _proprietary.Update(propietary, Context.CurrentUser.User.Id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/proprietary/5
        public async Task<bool>Delete(int id)
        {
            ResponseService<bool> Response = await _proprietary.Delete(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }
    }
}
