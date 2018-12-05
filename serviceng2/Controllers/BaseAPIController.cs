using gsproject;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace USoftEducation.Controllers
{
    public class BaseAPIController : ApiController
    {

        public T JSONGS<T>(T model) {
            var b = JsonConvert.SerializeObject(model, Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });

            var deserializedProduct = JsonConvert.DeserializeObject<T>(b);
            return deserializedProduct;
        }

        public T JSONGSTRING<T>(string str)
        {
            var deserializedProduct = JsonConvert.DeserializeObject<T>(str);
            return deserializedProduct;
        }

        public string JSON_to_STRING<T>(T model)
        {
            var b = JsonConvert.SerializeObject(model, Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       });
            return b;
        }

        public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        public InvalidModelStateResult ErrorMessage(string errormessage) {
            ModelState.AddModelError("", errormessage);
            return BadRequest(ModelState);
        }

        //public string GetDataBaseName(string dbcodeid="") {
        //    if(dbcodeid==string.Empty)
        //     dbcodeid = GetDataBaseCode();
        //    var dbcodename = System.Configuration.ConfigurationManager.AppSettings[dbcodeid];
        //    return dbcodename;
        //}

        public string GetDataBaseCode()
        {
            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            string dbcodeid = string.Empty;
            if (headers.Contains("dbcodeid"))
            {
                dbcodeid = headers.GetValues("dbcodeid").First();
            }
            return dbcodeid;
        }

        public string UploadImagefile(HttpPostedFile file, string dbcodeid)
        {
            string ext = Path.GetExtension(file.FileName);
            var fname = StaticData.RandomData() + ext;
            string pathfolder= HttpContext.Current.Server.MapPath("~/ReadWrite/" + dbcodeid);
            if (!Directory.Exists(pathfolder))
            {
                System.IO.Directory.CreateDirectory(pathfolder);
            }
            var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + dbcodeid + "/" + fname);
            file.SaveAs(filePath);

            if(file.ContentType== "image/jpeg")
                StaticData.ResizeImage(filePath, 550, 400);

            return fname;
        }

        public void DeleteImageByname(string imagename)
        {
            if (imagename != null)
            {
                var filePath = HttpContext.Current.Server.MapPath("~/ReadWrite/" + GetDataBaseCode() + "/" + imagename);
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch { }
            }
        }

        //public int GetStudentID() {
        //    var stuid = 0;
        //    var userWithClaims = (System.Security.Claims.ClaimsPrincipal)User;
        //    try
        //    {
        //        var sdetails = userWithClaims.Claims.First(c => c.Type == "studentid");
        //        stuid = Convert.ToInt32(sdetails.Value);
        //    }
        //    catch {
        //    }
        //    return stuid;
        //}

        public string GetUserDisplayName()
        {
            var userDisplayName = string.Empty;
            var userWithClaims = (System.Security.Claims.ClaimsPrincipal)User;
            try
            {
                var userdetails = userWithClaims.Claims.First(c => c.Type == "nameofuser");
                userDisplayName = userdetails.Value;
            }
            catch
            {
            }
            return userDisplayName;
        }
    }
}
