using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VModels;
using Business;
using System.Threading.Tasks;
using Ubikate.Core;
namespace Ubikate.Controllers
{
    public class BusinessController : BaseController
    {
        private BusinessBusiness _respository;
        public BusinessController()
        {
            _respository = new BusinessBusiness();
        }
        // GET api/bussiness
        [Authenticate]
        public async Task<IEnumerable<VMBusiness>> Get()
        {
            ResponseService<VMBusiness> Response = await _respository.Get(null);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.Result;
        }

        // GET api/bussiness/5
        [Authenticate]
        public async Task<VMBusiness> Get(int id)
        {
            ResponseService<VMBusiness> Response = await _respository.Get(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.Result.FirstOrDefault();
        }

        // POST api/bussiness
        [Authenticate]
        public async Task<bool> Post([FromBody]VMBusiness _bussiness)
        {
            ResponseService<bool> Response = await _respository.Save(_bussiness,(int)Context.CurrentUser.User.Id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;

        }

        // PUT api/bussiness/5
        [Authenticate]
        public async Task<bool> Put(int id, [FromBody]VMBusiness _bussiness)
        {
            ResponseService<bool> Response = await _respository.Update(_bussiness, (int)Context.CurrentUser.User.Id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }

        // DELETE api/bussiness/5
        [Authenticate]
        public async Task<bool> Delete(int id)
        {
            ResponseService<bool> Response = await _respository.Delete(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return true;
        }
    }
}
