 [Authorize]
    [RoutePrefix("api/Site")]
    public class SiteController : BaseAPIController
    {
        iSiteService _mainobj;
      
        public SiteController(iSiteService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(SiteModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.SiteModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.SiteModelid =Guid.NewGuid();

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());

                var webmanagerid = _mainobj.Create(model);
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
            var webmanager = _mainobj.GetById(new Guid(id));
            if (webmanager!=null)
            {
                var deserializedProduct = JSONGS<SiteModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager = _mainobj.GetAll();
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<SiteModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(SiteModel model)
        {
            var gid = model.SiteModelid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
			 	 dbmanager.SiteModelid = model.SiteModelid;
	 dbmanager.SiteLogo = model.SiteLogo;
               
                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager);
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }
    }


