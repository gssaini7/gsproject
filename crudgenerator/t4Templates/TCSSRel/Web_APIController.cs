 [Authorize]
    [RoutePrefix("api/TCSSRel")]
    public class TCSSRelController : BaseAPIController
    {
        iTCSSRelService _mainobj;
      
        public TCSSRelController(iTCSSRelService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(TCSSRelModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.TCSSRelModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.TCSSRelModelid =Guid.NewGuid();

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
            catch(Exception ex) {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager!=null)
            {
                var deserializedProduct = JSONGS<TCSSRelModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<TCSSRelModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(TCSSRelModel model)
        {
            var gid = model.TCSSRelModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
			 	 dbmanager.TCSSRelModelid = model.TCSSRelModelid;
	 dbmanager.Teacherid = model.Teacherid;
	 dbmanager.ClassModelid = model.ClassModelid;
	 dbmanager.SectionModelid = model.SectionModelid;
	 dbmanager.SubjectModelid = model.SubjectModelid;
               
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
        public bool Delete(TCSSRelModel model)
        {
            var gid = model.TCSSRelModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                
                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }
    }


