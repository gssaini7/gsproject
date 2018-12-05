using gsproject;
using Microsoft.AspNet.Identity;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace USoftEducation.Controllers
{
    
    [Authorize(Roles = "Notification")]
    [RoutePrefix("api/NotificationSchedule")]
    public class NotificationScheduleController : BaseAPIController
    {
        iNotificationScheduleService _mainobj;
        //iTCSSRelService _tcssobj;


        public NotificationScheduleController(iNotificationScheduleService imainobj
            //, iTCSSRelService itcssrelobj
            )
        {
            this._mainobj = imainobj;
            //this._tcssobj = itcssrelobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(NotificationScheduleModel model)
        {
            try
            {
                if (model == null)
                {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
                    
                ModelState.Remove("model.NotificationScheduleModelid");
                ModelState.Remove("model.notificationsentdate");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                

                model.NotificationScheduleModelid = Guid.NewGuid();
                model.notificationsentdate = DateTime.Now;
                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());

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

        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<NotificationScheduleModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager = _mainobj.GetAll(GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<NotificationScheduleModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetBlogSchedules")]
        public HttpResponseMessage GetBlogSchedules(string id) //current blog id
        {
           
            var webmanager = _mainobj.GetAll(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<NotificationScheduleModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(NotificationScheduleModel model)
        {
            var gid = model.NotificationScheduleModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.notificationsentdate = model.notificationsentdate;
                dbmanager.classname = model.classname;

                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager, GetDataBaseCode());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(NotificationScheduleModel model)
        {
            var gid = model.NotificationScheduleModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                var notificationcreator = dbmanager.createdBy.ToString();
                var currentuser = User.Identity.GetUserId();

                if (!User.IsInRole("Admin")) {
                    if (notificationcreator != currentuser) {
                        ModelState.AddModelError("", "You don't have permissions to delete.");
                        return BadRequest(ModelState);
                    }
                }
                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return Ok(); 
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }


        

    }
}
