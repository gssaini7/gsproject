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
    
    [Authorize(Roles = "Notification")]
    [RoutePrefix("api/blog")]
    public class BlogController : BaseAPIController
    {
        iBlogService _mainobj;
        iBlogTypeService _blogtypeobj;


        public BlogController(iBlogService imainobj, iBlogTypeService blogtypeobj)
        {
            this._mainobj = imainobj;
            this._blogtypeobj = blogtypeobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(BlogModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.BlogModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.BlogModelid = Guid.NewGuid();

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());
                model.createdByName = GetUserDisplayName();

                var blogtypedb = _blogtypeobj.GetById(model.BlogTypeModelid, GetDataBaseCode());
                if (blogtypedb == null) {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
                model.BlogTypeModelid = blogtypedb.BlogTypeModelid;

                var webmanagerid = _mainobj.Create(model, GetDataBaseCode());
                if (webmanagerid == Guid.Empty)
                {
                    ModelState.AddModelError("", "An error occured please contact administrator.");
                    return BadRequest(ModelState);
                }
                return Ok(webmanagerid);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            //Wreturn Ok();
        }

        [Route("Get")]
        public HttpResponseMessage GetByID(string id)
        {
            try
            {
                var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
                if (webmanager != null)
                {
                    var deserializedProduct = JSONGS<BlogModel>(webmanager);
                    return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
                }
            }
            catch { }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not found");
        }

        [Route("GetAll")]
        public HttpResponseMessage GetDetail(string id, string createrid = null)//id = blog type id
        {
            //var blogtype = _blogtypeobj.GetAllByName(blogtypename, GetDataBaseCode());
            //var blogtypeid = blogtype.BlogTypeModelid;
            string currentuser = string.Empty;
            if (createrid == null || createrid==string.Empty)
                currentuser = User.Identity.GetUserId();
            else
                currentuser = createrid;

            var webmanager = _mainobj.GetAllByType_CreatedBy(id, new Guid(currentuser), GetDataBaseCode());

            if (webmanager != null)
            {
                webmanager = webmanager.OrderByDescending(a => a.createdate);
                var deserializedProduct = JSONGS<IEnumerable<BlogModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        //[Route("GetAll")]
        //public HttpResponseMessage GetDetail(string id)//id = blog type id
        //{
        //    //var blogtype = _blogtypeobj.GetAllByName(blogtypename, GetDataBaseCode());
        //    //var blogtypeid = blogtype.BlogTypeModelid;
        //    var currentuser = User.Identity.GetUserId();
        //    var webmanager = _mainobj.GetAllByType_CreatedBy(id, new Guid(currentuser), GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<BlogModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}



        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(BlogModel model)
        {
            var gid = model.BlogModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.BlogModelid = model.BlogModelid;
                dbmanager.title = model.title;
                dbmanager.description = model.description;
                dbmanager.imagepath = model.imagepath;
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

        [Route("Deleteimage")]
        [HttpPost]
        public bool DeleteImage(BlogModel model)
        {
            var gid = model.BlogModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                DeleteImageByname(dbmanager.imagepath);
                dbmanager.imagepath = null;
                dbmanager.filetype = null;

                _mainobj.Update(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }



        [Route("Delete")]
        [HttpPost]
        public bool Delete(BlogModel model)
        {
            var gid = model.BlogModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                DeleteImageByname(dbmanager.imagepath);
                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }
        //private void DeleteImageByname(string imagename)
        //{
        //    if (imagename != null)
        //    {
        //        var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + GetDataBaseCode() + "/" + imagename);
        //        System.IO.File.Delete(filePath);
        //    }
        //}

        [AllowAnonymous]
        public bool Upload()
        {
            return UploadImage();
        }
        private bool UploadImage()
        {
            var result = false;
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode.UnsupportedMediaType)));
            }
            var context = HttpContext.Current.Request;
            if (context.Files.Count > 0)
            {
                var file = context.Files[0];
                var itemid = context.Form["BlogModelid"];
                var dbcodeid = context.Form["dbcodeid"];

                var dbmanager = _mainobj.GetById(new Guid(itemid), dbcodeid);
                if (dbmanager != null)
                {

                    var imgname = UploadImagefile(file, dbcodeid);
                    if (imgname != string.Empty)
                    {
                        dbmanager.imagepath = imgname;
                        dbmanager.filetype = file.ContentType;
                      result= _mainobj.Update(dbmanager, GetDataBaseCode());
                    }
                }
            }
            return result;
        }

        //private string UploadImagefile(HttpPostedFile file, string dbcodeid)
        //{
        //    string ext = Path.GetExtension(file.FileName);
        //    var fname = StaticData.RandomData() + ext;
        //    var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + dbcodeid + "/" + fname);
        //    file.SaveAs(filePath);
        //    StaticData.ResizeImage(filePath, 550, 400);
        //    return fname;
        //}

    }
}
