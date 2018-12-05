using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/Feedback")]
    public class FeedbackController : BaseAPIController
    {
        iFeedbackService _mainobj;

        public FeedbackController(iFeedbackService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Authorize(Roles = "Parent")]
        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(FeedbackModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.FeedbackModelid");
                ModelState.Remove("model.FeedbackDate");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.FeedbackModelid = Guid.NewGuid();
                model.FeedbackDate = DateTime.Now;
                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.FeedbackOpenDate = DateTime.Now;
                model.isPublished = true;
                model.createdBy =new Guid(User.Identity.GetUserId());


                var webmanagerid = _mainobj.Create(model, GetDataBaseCode());
                if (webmanagerid == Guid.Empty)
                {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        //[Route("Get")]
        //public HttpResponseMessage GetByID(string id)
        //{
        //    var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<FeedbackModel>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        [Authorize(Roles = "Feedback")]
        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager = _mainobj.GetAll(GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<FeedbackModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        //[Route("Edit")]
        //[HttpPost]
        //public async Task<IHttpActionResult> EditDetail(FeedbackModel model)
        //{
        //    var gid = model.FeedbackModelid;
        //    var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
        //    if (dbmanager != null)
        //    {
        //        dbmanager.FeedbackModelid = model.FeedbackModelid;
        //        dbmanager.FeedbackType = model.FeedbackType;
        //        dbmanager.StudentModelID = model.StudentModelID;
        //        dbmanager.FeedbackDate = model.FeedbackDate;
        //        dbmanager.FeedbackMessage = model.FeedbackMessage;


        //        _mainobj.Update(dbmanager, GetDataBaseCode());
        //        return Ok();
        //    }
        //    ModelState.AddModelError("", "An error occured please contact administrator.");
        //    return BadRequest(ModelState);
        //}

        [Authorize(Roles = "Feedback")]
        [Route("Delete")]
        [HttpPost]
        public bool Delete(FeedbackModel model)
        {
            var gid = model.FeedbackModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {

                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }
    }
}
