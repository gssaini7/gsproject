 [Authorize]
    [RoutePrefix("api/NotificationSchedule")]
    public class NotificationScheduleController : BaseAPIController
    {
        iNotificationScheduleService _mainobj;
      
        public NotificationScheduleController(iNotificationScheduleService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(NotificationScheduleModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.NotificationScheduleModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.NotificationScheduleModelid =Guid.NewGuid();

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());

                var webmanagerid = _mainobj.Create(model,GetDataBaseName());
                if (webmanagerid == Guid.Empty)
                {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex) {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id),GetDataBaseName());
            if (webmanager!=null)
            {
                var deserializedProduct = JSONGS<NotificationScheduleModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager = _mainobj.GetAll(GetDataBaseName());
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
            var dbmanager = _mainobj.GetById(gid,GetDataBaseName());
            if (dbmanager != null)
            {
			 	 dbmanager.NotificationScheduleModelid = model.NotificationScheduleModelid;
	 dbmanager.notificationsentdate = model.notificationsentdate;
	 dbmanager.classname = model.classname;
               
                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager,GetDataBaseName());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }
    }


