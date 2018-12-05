 [Authorize]
    [RoutePrefix("api/MainDatabases")]
    public class MainDatabasesController : BaseAPIController
    {
        iMainDatabasesService _mainobj;
      
        public MainDatabasesController(iMainDatabasesService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(MainDatabasesModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.MainDatabasesModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.MainDatabasesModelid =Guid.NewGuid();

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
                var deserializedProduct = JSONGS<MainDatabasesModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<MainDatabasesModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(MainDatabasesModel model)
        {
            var gid = model.MainDatabasesModelid;
            var dbmanager = _mainobj.GetById(gid,GetDataBaseName());
            if (dbmanager != null)
            {
			 	 dbmanager.MainDatabasesModelid = model.MainDatabasesModelid;
	 dbmanager.IDataSource = model.IDataSource;
	 dbmanager.IInitialCatalog = model.IInitialCatalog;
	 dbmanager.IUsername = model.IUsername;
	 dbmanager.IPassword = model.IPassword;
	 dbmanager.IName = model.IName;
	 dbmanager.IUIDCode = model.IUIDCode;
               
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


