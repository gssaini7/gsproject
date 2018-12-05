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
    [RoutePrefix("api/BlogType")]
    public class BlogTypeController : BaseAPIController
    {
        iBlogTypeService _mainobj;
        iBlogService _blogObj;

        public BlogTypeController(iBlogTypeService imainobj,iBlogService iblogObj)
        {
            this._mainobj = imainobj;
            this._blogObj = iblogObj;
        }

        [Authorize(Roles = "Legitimate")]
        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(BlogTypeModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.BlogTypeModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.BlogTypeModelid = Guid.NewGuid();

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

        [Authorize(Roles = "Notification")]
        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<BlogTypeModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Authorize(Roles = "Notification")]
        [Route("GetAll")]
        public HttpResponseMessage GetDetail()
        {
            var webmanager = _mainobj.GetAll(GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<BlogTypeModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Authorize(Roles = "Legitimate")]
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(BlogTypeModel model)
        {
            var gid = model.BlogTypeModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.BlogTypeName = model.BlogTypeName;
                dbmanager.BlogTypeProperty = model.BlogTypeProperty;
                dbmanager.BlogTypeDisplayName = model.BlogTypeDisplayName;
                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                _mainobj.Update(dbmanager, null);
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Legitimate")]
        [Route("Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(BlogTypeModel model)
        {
            var gid = model.BlogTypeModelid;
            var blogs = _blogObj.GetAllByType(gid.ToString(), GetDataBaseCode());
            if (blogs != null) {
                if (blogs.Any()) {
                    ModelState.AddModelError("", "There are related "+ blogs.Count() + " blog(s), please delete that first.");
                    return BadRequest(ModelState);
                }
            }

            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }
    }
}
