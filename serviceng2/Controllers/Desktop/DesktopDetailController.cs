using gsproject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using USoftEducation.Models;

namespace USoftEducation.Controllers
{
    [Authorize]
    [RoutePrefix("api/desk")]
    public class DesktopDetailController : BaseAPIController
    {
        //private ApplicationUserManager _userManager;
        //private ApplicationRoleManager _roleManager;

        iStudentService _studentobj;
        iTCSSRelService _tcssobj;
        iClassService _classbj;



        //iSchoolService _schoolobj;
        //iAttendanceService _Attendanceobj;
        //iLedgerSumaryService _LedgerSumaryobj;
        //iLedgerDetailService _LedgerDetailobj;
        //iAcademicService _Academicobj;
        //iMainDatabasesService _MainDatabasesService;
        //iClassService _classbj;




        public DesktopDetailController(iStudentService istudentobj
        ,iTCSSRelService tcssobj, iClassService classbj

        //    , iStudentService studentobj,
        //iSchoolService schoolobj,
        //iAttendanceService Attendanceobj,
        //iLedgerSumaryService LedgerSumaryobj,
        //iLedgerDetailService LedgerDetailobj,
        //iAcademicService Academicobj,
        //iMainDatabasesService MainDatabasesService,
        //iClassService classbj
        )
        {
            this._studentobj = istudentobj;
            this._tcssobj = tcssobj;
            //this._schoolobj = schoolobj;
            //this._Attendanceobj = Attendanceobj;
            //this._LedgerSumaryobj = LedgerSumaryobj;
            //this._LedgerDetailobj = LedgerDetailobj;
            //this._Academicobj = Academicobj;
            //this._MainDatabasesService = MainDatabasesService;
            this._classbj = classbj;

        }



        #region API's

        [Route("GetStudentsByTeacher")]
        public HttpResponseMessage GetStudentsByTeacher()
        {
            IEnumerable<StudentModel> students = null;
            var isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                students = _studentobj.GetAll(GetDataBaseCode());
            }
            else
            {

                var teacherid = User.Identity.GetUserId();
                var teacherallrecords = _tcssobj.GetAllByAdmin(new Guid(teacherid), GetDataBaseCode());
                if (teacherallrecords == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
                }
                var distinctteacherrecords = teacherallrecords.GroupBy(g => new { g.ClassModelid, g.SectionModelid }).Select(c => c.First()).ToList();
                List<ClassSectionModel> classsectiondistinctm = distinctteacherrecords.Select(x => new ClassSectionModel { ClassModelid = x.ClassModelid, SectionModelid = x.SectionModelid }).ToList();
                students = _studentobj.GetAllByTeacher(classsectiondistinctm, GetDataBaseCode());
            }
            if (students != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<StudentModel>>(students);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetTeacherByCSS")]
        public async Task<IHttpActionResult> GetTeacherByCSS(ClassSectionSubjectModel cssmodel)
        {
            var isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                return Ok();
            }
            else
            {
                var teacherid = User.Identity.GetUserId();
                var teacherallrecords = _tcssobj.GetAllByAdmin(new Guid(teacherid), GetDataBaseCode());
                if (teacherallrecords == null)
                {
                    var singlerecord = teacherallrecords.Where(c => (c.ClassModelid == cssmodel.ClassModelid && c.SectionModelid == cssmodel.SectionModelid && c.SubjectModelid == cssmodel.SubjectModelid)).FirstOrDefault();
                    if (singlerecord != null)
                        return Ok();
                }
            }
            ModelState.AddModelError("", "An error occured please contact administrator.");
            return BadRequest(ModelState);
        }


        //[Authorize(Roles = "Admin")]
        [Route("GetAllClasses")]
        public HttpResponseMessage GetAllClasses()
        {
            var webmanager = _classbj.GetAllClass(GetDataBaseCode());

            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<ClassModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        //[Authorize(Roles = "Admin")]
        [Route("GetAllSections")]
        public HttpResponseMessage GetAllSections()
        {
            var webmanager = _classbj.GetAllSection(GetDataBaseCode());

            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<SectionModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        //[Authorize(Roles = "Admin")]
        [Route("GetAllSubjects")]
        public HttpResponseMessage GetAllSubjects()
        {
            var webmanager = _classbj.GetAllSubject(GetDataBaseCode());

            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<SubjectModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }



        #endregion


    }
}
