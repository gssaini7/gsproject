 [Authorize]
    [RoutePrefix("api/Album")]
    public class AlbumController : BaseAPIController
    {
        iAlbumService _mainobj;
      
        public AlbumController(iAlbumService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(AlbumModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.AlbumModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.AlbumModelid =Guid.NewGuid();

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
                var deserializedProduct = JSONGS<AlbumModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<AlbumModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(AlbumModel model)
        {
            var gid = model.AlbumModelid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
			 	 dbmanager.AlbumModelid = model.AlbumModelid;
	 dbmanager.AlbumName = model.AlbumName;
               
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


