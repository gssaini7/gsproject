 [Authorize]
    [RoutePrefix("api/Settings")]
    public class SettingsController : BaseAPIController
    {
        iSettingsService _mainobj;
      
        public SettingsController(iSettingsService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(SettingsModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.SettingsModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.SettingsModelid =Guid.NewGuid();

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());

                var webmanagerid = _mainobj.Create(model,GetDataBaseCode());
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
            var webmanager = _mainobj.GetById(new Guid(id),GetDataBaseCode());
            if (webmanager!=null)
            {
                var deserializedProduct = JSONGS<SettingsModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<SettingsModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(SettingsModel model)
        {
            var gid = model.SettingsModelid;
            var dbmanager = _mainobj.GetById(gid,GetDataBaseCode());
            if (dbmanager != null)
            {
			 	 dbmanager.SettingsModelid = model.SettingsModelid;
	 dbmanager.SettingsType = model.SettingsType;
	 dbmanager.SettingsContent = model.SettingsContent;
               
                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager,GetDataBaseCode());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }
    

	
        [Route("Delete")]
        [HttpPost]
        public bool Delete(SettingsModel model)
        {
            var gid = model.SettingsModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {

                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }
}

