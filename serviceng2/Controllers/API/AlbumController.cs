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
    [Authorize(Roles = "ImageGallery")]
    [RoutePrefix("api/Album")]
    public class AlbumController : BaseAPIController
    {
        iAlbumService _mainobj;
        iImageGalleryService _imagesobj;


        public AlbumController(iAlbumService imainobj, iImageGalleryService iimagesobj)
        {
            this._mainobj = imainobj;
            this._imagesobj = iimagesobj;
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

                model.AlbumModelid = Guid.NewGuid();

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
                var deserializedProduct = JSONGS<AlbumModel>(webmanager);
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
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.AlbumModelid = model.AlbumModelid;
                dbmanager.AlbumName = model.AlbumName;

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
        public async Task<IHttpActionResult> Delete(AlbumModel model)
        {
            var gid = model.AlbumModelid;
            var images = _imagesobj.GetAllByAlbumId(gid, GetDataBaseCode());
            if (images != null)
            {
                if (images.Any())
                {
                    ModelState.AddModelError("", "There are related " + images.Count() + " image(s), please delete that first.");
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
