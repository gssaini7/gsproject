 [Authorize]
    [RoutePrefix("api/Menu")]
    public class MenuController : BaseAPIController
    {
        iMenuService _mainobj;
      
        public MenuController(iMenuService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(MenuModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.MenuModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.MenuModelid =Guid.NewGuid();

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
                var deserializedProduct = JSONGS<MenuModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<MenuModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(MenuModel model)
        {
            var gid = model.MenuModelid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
			 	 dbmanager.MenuModelid = model.MenuModelid;
	 dbmanager.MenuName = model.MenuName;
	 dbmanager.MenuClass = model.MenuClass;
               
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


