using gsproject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
using USoftEducation.Models;

namespace USoftEducation.Controllers
{
    [Authorize]
    [RoutePrefix("api/page")]
    public class PageController : BaseAPIController
    {
        iPageService _mainobj;
      
        public PageController(iPageService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(PageModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.PageModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pageurl= StaticData.Sanitizeurl(model.pageurl);
                var anypage = _mainobj.GetByURL(pageurl, GetDataBaseCode());

                if (anypage != null) {
                    return ErrorMessage("URL already existed.");
                }

                model.PageModelid =Guid.NewGuid();
                model.pageurl = pageurl;
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
            catch(Exception ex) {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

      

        [Route("GetPage")]
        public HttpResponseMessage GetByID(string id)
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager!=null)
            {
                var deserializedProduct = JSONGS<PageModel>(webmanager);
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
                var deserializedProduct = JSONGS<IEnumerable<PageModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(PageModel model)
        {
            var gid = model.PageModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                var pageurl = StaticData.Sanitizeurl(model.pageurl);
                
                if (dbmanager.pageurl != pageurl)
                {
                    var anypage = _mainobj.GetByURL(pageurl, GetDataBaseCode());
                    if (anypage != null)
                    {
                        if (anypage.PageModelid != dbmanager.PageModelid)
                        {
                            ModelState.AddModelError("", "URL already existed.");
                            return BadRequest(ModelState);
                        }
                    }
                }

                dbmanager.pagecontent = model.pagecontent;
                dbmanager.PageTitle = model.PageTitle;
                dbmanager.pageurl = pageurl;
                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;
                _mainobj.Update(dbmanager, GetDataBaseCode());
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        //[Route("Delete")]
        //[HttpPost]
        //public bool DeleteRecord(ClientDetailModel model)
        //{
        //    var gid = model.ItemID;
        //    var dbmanager = _mainobj.GetById(gid);
        //    if (dbmanager != null)
        //    {
        //        _mainobj.Delete(dbmanager.ItemID);
        //        return true;
        //    }
        //    return false;
        //}

        //[AllowAnonymous]
        //[Route("GetItems")]
        //public HttpResponseMessage GetItemsForApp()
        //{
        //    var webmanager = _mainobj.GetAll();
        //    if (webmanager.Any())
        //    {
        //        var newitemslist = new List<ItemToDisplay>();
        //        foreach (var item in webmanager)
        //        {

        //            if (item.ItemMainTypes.isPublished)
        //            {
        //                if (item.isPublished)
        //                {
        //                    var newitem = new ItemToDisplay()
        //                    {
        //                        ItemID = item.ItemID,
        //                        ItemBasePrice = item.ItemBasePrice,
        //                        ItemName = item.ItemName,
        //                        ItemMainTypesName = item.ItemMainTypes.ItemTypesName,
        //                        ItemMainTypesImage = item.ItemMainTypes.ItemTypesImage,
        //                        ItemMainTypesFixed = item.ItemMainTypes.ItemTypesFixed,
        //                        ItemCurrentPrice = item.ItemCurrentPrice,
        //                    };
        //                    newitemslist.Add(newitem);
        //                }
        //            }
        //        }
        //        if (newitemslist.Any())
        //            return Request.CreateResponse(HttpStatusCode.OK, newitemslist);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}



    }
}
