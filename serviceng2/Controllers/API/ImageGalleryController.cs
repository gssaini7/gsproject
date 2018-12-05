using gsproject;
using Microsoft.AspNet.Identity;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace USoftEducation.Controllers
{
    [Authorize(Roles = "ImageGallery")]
    [RoutePrefix("api/ImageGallery")]
    public class ImageGalleryController : BaseAPIController
    {
        iImageGalleryService _mainobj;

        public ImageGalleryController(iImageGalleryService imainobj)
        {
            this._mainobj = imainobj;
        }

        //[Route("Create")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SaveDetail(ImageGalleryModel model)
        //{
        //    try
        //    {
        //        if (model == null)
        //            return null;
        //        ModelState.Remove("model.ImageGalleryModelid");
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        model.ImageGalleryModelid = Guid.NewGuid();

        //        model.createdate = DateTime.Now;
        //        model.LastUpdatedate = DateTime.Now;
        //        model.createdBy = new Guid(User.Identity.GetUserId());

        //        var webmanagerid = _mainobj.Create(model, GetDataBaseCode());
        //        if (webmanagerid == Guid.Empty)
        //        {
        //            ModelState.AddModelError("", "An error occured please contact administrator.");
        //            return BadRequest(ModelState);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "An error occured please contact administrator.");
        //        return BadRequest(ModelState);
        //    }
        //    return Ok();
        //}

        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<ImageGalleryModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }


        [Route("GetAll")]
        public HttpResponseMessage GetDetail(string id)
        {
            var webmanager = _mainobj.GetAllByAlbumId(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                webmanager = webmanager.OrderByDescending(d => d.createdate);
                var deserializedProduct = JSONGS<IEnumerable<ImageGalleryModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        //[Route("GetAll")]
        //public HttpResponseMessage GetDetail()
        //{
        //    var webmanager = _mainobj.GetAll();
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<ImageGalleryModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}
        //[Route("Edit")]
        //[HttpPost]
        //public async Task<IHttpActionResult> EditDetail(ImageGalleryModel model)
        //{
        //    var gid = model.ImageGalleryModelid;
        //    var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
        //    if (dbmanager != null)
        //    {
        //        dbmanager.ImageGalleryModelid = model.ImageGalleryModelid;
        //        dbmanager.ImageName = model.ImageName;

        //        dbmanager.isPublished = model.isPublished;
        //        dbmanager.LastUpdatedate = DateTime.Now;
        //        dbmanager.remarks = model.remarks;

        //        _mainobj.Update(dbmanager, GetDataBaseCode());
        //        return Ok();
        //    }
        //    ModelState.AddModelError("", "An error occured please contact administrator.");
        //    return BadRequest(ModelState);
        //}

        [Route("Upload")]
        public async Task<IHttpActionResult> Upload()
        {
            try
            {
                HttpRequestMessage request = this.Request;
                if (!request.Content.IsMimeMultipartContent())
                {
                    //throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
                var result = UploadImage();
                if (result)
                    return Ok();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return BadRequest(ModelState);
            }

            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }
        private bool UploadImage()
        {
            var result = false;
            //HttpRequestMessage request = this.Request;
            //if (!request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
            //}
            var context = HttpContext.Current.Request;
            if (context.Files.Count > 0)
            {
                var file = context.Files[0];
                var AlbumModelid = context.Form["AlbumModelid"];
                var dbcodeid = context.Form["dbcodeid"];

                ImageGalleryModel model = new ImageGalleryModel()
                {
                    AlbumModelid = new Guid(AlbumModelid),
                    ImageGalleryModelid = Guid.NewGuid(),
                    createdate = DateTime.Now,
                    LastUpdatedate = DateTime.Now,
                    createdBy = new Guid(User.Identity.GetUserId()),
                    ImageName = UploadImagefile(file, dbcodeid),
                };
                var webmanagerid = _mainobj.Create(model, dbcodeid);
                if (webmanagerid == Guid.Empty)
                {
                    result = false;
                }
                result = true;
            }
            return result;
        }

        //private string UploadImagefile(HttpPostedFile file)
        //{
        //    string ext = Path.GetExtension(file.FileName);
        //    var fname = StaticData.RandomData() + ext;
        //    var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + fname);
        //    file.SaveAs(filePath);
        //    StaticData.ResizeImage(filePath, 0, 0);

        //    var filePathThumb = HttpContext.Current.Server.MapPath("~/ReadWrite/Thumb/" + fname);
        //    StaticData.ResizeImage(filePath, filePathThumb, 90, 90);
        //    return fname;
        //}

        [Route("Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteImage(ImageGalleryModel model)
        {
            try
            {
                var gid = model.ImageGalleryModelid;
                var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
                if (dbmanager != null)
                {
                    DeleteImageByname(dbmanager.ImageName);
                    _mainobj.Delete(dbmanager.ImageGalleryModelid, GetDataBaseCode());
                    return Ok();

                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.ToString());
                return BadRequest(ModelState);
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        //private void DeleteImageByname(string imagename)
        //{
        //    if (imagename != null)
        //    {
        //        var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + imagename);
        //        System.IO.File.Delete(filePath);
        //        var filePaththumb = HttpContext.Current.Server.MapPath("~/ReadWrite/Thumb/" + imagename);
        //        System.IO.File.Delete(filePaththumb);
        //    }
        //}
    }
}
