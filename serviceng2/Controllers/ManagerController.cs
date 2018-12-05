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
        [HttpPost]
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
        [Route("Edit")]
        [System.Web.Http.HttpPut]
        public bool EditDetail(Manager model)
        {
            var gid = model.Managerid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
                dbmanager.isPublished = model.isPublished;
                dbmanager.Managercontent = model.Managercontent;
                dbmanager.Managertype = model.Managertype;
                _mainobj.Update(model.Managerid, dbmanager);
                return true;
            }
            return false;
        }

        [Route("Delete")]
        [HttpPost]
        public bool DeleteRecord(Manager model)
        {
            var gid = model.Managerid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
                _mainobj.Delete(dbmanager.Managerid);
                return true;
            }
            return false;
        }


        //[Route("Delete")]
        //public bool DeleteRecord(Guid id)
        //{
        //    //var gid = model.Managerid;
        //    var dbmanager = _mainobj.GetById(id);
        //    if (dbmanager != null)
        //    {
        //        _mainobj.Delete(dbmanager.Managerid);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
