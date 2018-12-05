using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace USoftEducation.Controllers
{
    [Authorize]
    [RoutePrefix("api/Manager")]
    public class ManagerController : ApiController
    {
        iDbManagerService _mainobj;

       
        public ManagerController(iDbManagerService imainobj)
        { this._mainobj = imainobj; }

        [Route("Create")]
        public bool SaveDetail(Manager model)
        {
            var webmanagerid = _mainobj.Create(model);
            var result = true;
            if (webmanagerid == Guid.Empty)
            {
                result = false;
            }
            return result;
        }

        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager =  _mainobj.GetAll();

            if (webmanager!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, webmanager);
            }
        
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
    }
}
