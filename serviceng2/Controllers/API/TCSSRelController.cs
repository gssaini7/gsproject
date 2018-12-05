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
    [Authorize]
    [RoutePrefix("api/TCSSRel")]
    public class TCSSRelController : BaseAPIController
    {
        iTCSSRelService _mainobj;

        public TCSSRelController(iTCSSRelService imainobj)
        {
            this._mainobj = imainobj;
        }

        [Authorize(Roles = "Admin")]
        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(TCSSRelModel model)
        {
            try
            {
                if (model == null)
                    return null;
                ModelState.Remove("model.TCSSRelModelid");
                ModelState.Remove("model.ClassModelid");
                ModelState.Remove("model.SectionModelid");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var previouscheck = GetTeacherRecord(model.Teacherid.ToString(), GetDataBaseCode());
                if (previouscheck != null)
                {
                    var single = previouscheck.Where(a => a.ClassModelid == model.ForClass.ClassModelid && a.SectionModelid == model.ForSection.SectionModelid && a.SubjectModelid == model.ForSubject.SubjectModelid).FirstOrDefault();
                    if (single != null)
                    {
                        ModelState.AddModelError("", "Record already exists of " + single.ClassModelid + "-" + single.SectionModelid + "-" + single.SubjectModelid);
                        return BadRequest(ModelState);
                    }
                }

                model.TCSSRelModelid = Guid.NewGuid();

                model.createdate = DateTime.Now;
                model.LastUpdatedate = DateTime.Now;
                model.createdBy = new Guid(User.Identity.GetUserId());
                model.ClassModelid = model.ForClass.ClassModelid;
                model.SectionModelid = model.ForSection.SectionModelid;
                model.SubjectModelid = model.ForSubject.SubjectModelid;

                model.ForClass = null;
                model.ForSection = null;
                model.ForSubject = null;


                var webmanagerid = _mainobj.Create(model, null);
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

        [Authorize(Roles = "Admin")]
        [Route("Get")]
        public HttpResponseMessage GetByID(string id) 
        {
            var webmanager = _mainobj.GetById(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<TCSSRelModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        
        [Route("GetAll")]
        public HttpResponseMessage GetDetail(string id) //by teacher id
        {
            var webmanager = _mainobj.GetAllByAdmin(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<TCSSRelModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> EditDetail(TCSSRelModel model)
        {
            var gid = model.TCSSRelModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {
                dbmanager.TCSSRelModelid = model.TCSSRelModelid;
                dbmanager.Teacherid = model.Teacherid;
                dbmanager.ClassModelid = model.ForClass.ClassModelid;
                dbmanager.SectionModelid = model.ForSection.SectionModelid;
                dbmanager.SubjectModelid = model.ForSubject.SubjectModelid;

                dbmanager.isPublished = model.isPublished;
                dbmanager.LastUpdatedate = DateTime.Now;
                dbmanager.remarks = model.remarks;

                var previouscheck = GetTeacherRecord(model.Teacherid.ToString(), null);
                if (previouscheck != null)
                {
                    var single = previouscheck.Where(a => a.ClassModelid == model.ForClass.ClassModelid && a.SectionModelid == model.ForSection.SectionModelid
                    && a.SubjectModelid == model.ForSubject.SubjectModelid && a.TCSSRelModelid != dbmanager.TCSSRelModelid).FirstOrDefault();
                    if (single != null)
                    {
                        ModelState.AddModelError("", "Record already exists of " + single.ClassModelid + "-" + single.SectionModelid + "-" + single.SubjectModelid);
                        return BadRequest(ModelState);
                    }
                }

                _mainobj.Update(dbmanager, null);
                return Ok();
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpPost]
        public bool Delete(TCSSRelModel model)
        {
            var gid = model.TCSSRelModelid;
            var dbmanager = _mainobj.GetById(gid, GetDataBaseCode());
            if (dbmanager != null)
            {

                _mainobj.Delete(dbmanager, GetDataBaseCode());
                return true;
            }
            return false;
        }

        
        [Route("GetAllByLogedInUser")]
        public HttpResponseMessage GetAllByLogedInUser()
        {
            var id = User.Identity.GetUserId();
            return GetDetail(id);
            //var webmanager = _mainobj.GetAllByAdmin(new Guid(id), GetDataBaseCode());
            //if (webmanager != null)
            //{
            //    var deserializedProduct = JSONGS<IEnumerable<TCSSRelModel>>(webmanager);
            //    var list = deserializedProduct.GroupBy(x => x.ClassModelid).Select(x => x.First()).ToList();
            //    return Request.CreateResponse(HttpStatusCode.OK, list);
            //}

            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        private IEnumerable<TCSSRelModel> GetTeacherRecord(string id, string dbcode) //by teacher id
        {
            var webmanager = _mainobj.GetAllByAdmin(new Guid(id), dbcode);
            if (webmanager != null)
            {
                if (webmanager.Any())
                    return webmanager;
            }
            return null;
        }
        //[Route("GetTeacherSections")]
        //public HttpResponseMessage GetTeacherSections(string id)
        //{
        //    var webmanager = _mainobj.GetAllSection(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SectionModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("GetTeacherSubjects")]
        //public HttpResponseMessage GetTeacherSubjects(string id)
        //{
        //    var webmanager = _mainobj.GetAllSubject(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SubjectModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}
    }
}
