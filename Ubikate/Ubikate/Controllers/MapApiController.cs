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
    public class MapApiController : BaseController
    {
        BusinessBusiness _repository;
        public MapApiController()
        {
            _repository = new BusinessBusiness();
        }
        // GET api/mapapi
        public async Task<IEnumerable<BusinessMap>> Get()
        {
            ResponseService<BusinessMap> Response = await _repository.getBusinessMap();
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.Result;
        }

        // GET api/mapapi/5
        public async Task<DetailBusinessMap> Get(int id)
        {
            ResponseService<DetailBusinessMap> Response=await _repository.getBusinessMapId(id);
            if (Response.Error)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Response.Message));
            return Response.Result.FirstOrDefault();
        }

        // POST api/mapapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/mapapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mapapi/5
        public void Delete(int id)
        {
        }
    }
}
