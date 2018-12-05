using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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

    [RoutePrefix("api/Settings")]
    public class SettingsController : BaseAPIController
    {
        iSettingsService _mainobj;

        public SettingsController(iSettingsService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Route("Get")]
        public HttpResponseMessage GetType(string type)
        {
            //if (!User.IsInRole("Settings-" + type))
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized.");
            //}

            var webmanager = _mainobj.GetSettingByType(type, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<SettingsModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        //[Route("api/Settings/GetOnlinePayment")]
        //public HttpResponseMessage GetOnlinePayment()
        //{
        //    var webmanager = _mainobj.GetSettingByType(SettingsType.OnlinePayment.ToString(), GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGSTRING<OnlinePaymentModel>(webmanager.SettingsContent);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("api/Settings/GetMail")]
        //public HttpResponseMessage GetMail()
        //{
        //    var webmanager = _mainobj.GetSettingByType(SettingsType.Mail.ToString(), GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGSTRING<MailModel>(webmanager.SettingsContent);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("api/Settings/GetSMS")]
        //public HttpResponseMessage GetSMS()
        //{
        //    var webmanager = _mainobj.GetSettingByType(SettingsType.SMS.ToString(), GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGSTRING<SMSModel>(webmanager.SettingsContent);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(SettingsModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.SettingsModelid");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.SettingsModelid = Guid.NewGuid();

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



        //[Route("Get")]
        //public HttpResponseMessage GetByID(string id)
        //{
        //    var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<SettingsModel>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("GetAll")]
        //public HttpResponseMessage GetDetail()
        //{
        //    var webmanager = _mainobj.GetAll(GetDataBaseCode());
        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SettingsModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }

        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(SettingsModel model)
        {
            var gid = model.SettingsModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.SettingsModelid = model.SettingsModelid;
                dbmanager.SettingsContent = model.SettingsContent;
                _mainobj.Update(dbmanager, null);
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }



        //[Route("Delete")]
        //[HttpPost]
        //public bool Delete(SettingsModel model)
        //{
        //    var gid = model.SettingsModelid;
        //    var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
        //    if (dbmanager != null)
        //    {

        //        _mainobj.Delete(dbmanager, GetDataBaseCode());
        //        return true;
        //    }
        //    return false;
        //}
    }

    public enum SettingsType {
        SMS,
        Mail,
        OnlinePayment
    }

}
