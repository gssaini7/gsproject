 [Authorize]
    [RoutePrefix("api/MenuItem")]
    public class MenuItemController : BaseAPIController
    {
        iMenuItemService _mainobj;
      
        public MenuItemController(iMenuItemService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(MenuItemModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.MenuItemModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.MenuItemModelid =Guid.NewGuid();

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
                var deserializedProduct = JSONGS<MenuItemModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<MenuItemModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(MenuItemModel model)
        {
            var gid = model.MenuItemModelid;
            var dbmanager = _mainobj.GetById(gid);
            if (dbmanager != null)
            {
			 	 dbmanager.MenuItemModelid = model.MenuItemModelid;
	 dbmanager.ItemTitle = model.ItemTitle;
	 dbmanager.ItemText = model.ItemText;
	 dbmanager.ItemLink = model.ItemLink;
	 dbmanager.Classli = model.Classli;
	 dbmanager.Classa = model.Classa;
	 dbmanager.Parentid = model.Parentid;
	 dbmanager.MenuModel_MenuModelid = model.MenuModel_MenuModelid;
               
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


