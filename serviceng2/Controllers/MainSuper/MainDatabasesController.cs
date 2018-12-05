using gsproject;
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
    [Authorize(Roles = "Legitimate")]
    [RoutePrefix("api/institutes")]
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

               

                model.MainDatabasesModelid = Guid.NewGuid();
                var uniquecode = UniqueCodeForInstitute(model.IUIDCode, model, true);
                if (uniquecode == string.Empty)
                    return BadRequest("Code already existed.");

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());

                model.IUIDCode = uniquecode;

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
                var deserializedProduct = JSONGS<MainDatabasesModel>(webmanager);
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
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                var uniquecode = UniqueCodeForInstitute(model.IUIDCode, dbmanager, false);
                if(uniquecode==string.Empty)
                    return BadRequest("Code already existed.");

                dbmanager.IDataSource = model.IDataSource;
                dbmanager.IInitialCatalog = model.IInitialCatalog;
                dbmanager.IUsername = model.IUsername;
                dbmanager.IPassword = model.IPassword;
                dbmanager.IName =model.IName;
                dbmanager.IUIDCode = uniquecode;

                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager, GetDataBaseCode());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        private string UniqueCodeForInstitute(string icode, MainDatabasesModel dbmodel, bool isFirstTime)
        {
            var ocode = StaticData.SanitizeAlphanumeric(icode);
            var dbname = GetDataBaseCode();
            if (!isFirstTime)
                dbname = string.Empty;
            var otherrecord = _mainobj.GetByName(ocode, dbname);
            if (otherrecord != null)
            {
                if (otherrecord.MainDatabasesModelid != dbmodel.MainDatabasesModelid)
                    return string.Empty;
            }
            return ocode;
        }
    }
}
