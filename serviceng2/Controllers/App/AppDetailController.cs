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
    [Authorize(Roles = "Parent")]
    [RoutePrefix("api/appdetail")]
    public class AppDetailController : BaseAPIController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        iStudentService _studentobj;
        iSchoolService _schoolobj;
        iAttendanceService _Attendanceobj;
        iLedgerSumaryService _LedgerSumaryobj;
        iLedgerDetailService _LedgerDetailobj;
        iAcademicService _Academicobj;
        iAcademicDetailService _AcademicDetailobj;

        iMainDatabasesService _MainDatabasesService;
        iClassService _classbj;
        iAlbumService _albumobj;
        iImageGalleryService _imggalleryobj;
        iSettingsService _settingsobj;


        iNotificationScheduleService _iNotificationScheduleService;
        

        public AppDetailController(iStudentService istudentobj, iStudentService studentobj,
        iSchoolService schoolobj,
        iAttendanceService Attendanceobj,
        iLedgerSumaryService LedgerSumaryobj,
        iLedgerDetailService LedgerDetailobj,
        iAcademicService Academicobj,
        iMainDatabasesService MainDatabasesService,
        iClassService classbj,
        iNotificationScheduleService notificationscheduleService,
        iAlbumService ialbumobj,
        iImageGalleryService iimggalleryobj,
        iSettingsService isettingsobj,
        iAcademicDetailService iAcademicDetailobj


        )
        {
            this._studentobj = istudentobj;
            this._schoolobj = schoolobj;
            this._Attendanceobj = Attendanceobj;
            this._LedgerSumaryobj = LedgerSumaryobj;
            this._LedgerDetailobj = LedgerDetailobj;
            this._Academicobj = Academicobj;
            this._MainDatabasesService = MainDatabasesService;
            this._classbj = classbj;
            this._iNotificationScheduleService = notificationscheduleService;
            this._albumobj = ialbumobj;
            this._imggalleryobj = iimggalleryobj;
            this._settingsobj = isettingsobj;
            this._AcademicDetailobj = iAcademicDetailobj;



        }

        #region user login

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private ApplicationUserManager UserManagerMethod(MainDatabasesModel dbcode)
        {
            if (_userManager == null)
            {
                var code = dbcode;
                //var code = Request.Form.Get("SchoolCode");//Get from FORM\QueryString\Session whatever you wants
                if (code != null)
                {
                    var appDbContext = new ApplicationDbContext(code);

                    Request.GetOwinContext().Set<ApplicationDbContext>(appDbContext);
                    Request.GetOwinContext().Set<ApplicationUserManager>(new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext))); //OR USE your specified create Method
                }
                _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            return _userManager;
        }

        private ApplicationRoleManager RoleManagerMethod(string dbcode)
        {
            if (_roleManager == null)
            {
                var code = dbcode;
                if (code != null || code != string.Empty)
                {
                    var appDbContext = new ApplicationDbContext(code.ToString());

                    Request.GetOwinContext().Set<ApplicationDbContext>(appDbContext);
                    Request.GetOwinContext().Set<ApplicationRoleManager>(new ApplicationRoleManager(new RoleStore<IdentityRole>(appDbContext))); //OR USE your specified create Method
                }
                _roleManager = Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            return _roleManager;
        }
        // api/appdetail/AppLogin
        [AllowAnonymous]
        [Route("AppLogin")]
        public async Task<IHttpActionResult> AppLogin(LoginAppModel model)
        {
            try
            {
                if (model == null)
                    return null;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbcode = model.dbcode;
                var dbcodename = GetDatabasesResult(dbcode);

                if (dbcodename == null)
                {
                    return BadRequest("UserName/UID not valid.");
                }
                model.Mobile = model.Mobile.Trim();

                var usermanagerlocal = UserManagerMethod(dbcodename);
                var user = await UserManager.FindByNameAsync(model.Mobile);

                var mobilenumber = string.Empty;

                if (user == null)
                {
                    //user not present in user table, check in student table
                    var student = GetStudent(model.Mobile, dbcodename.MainDatabasesModelid.ToString());
                    if (student == null)
                    { // user not exist in our records
                        return BadRequest("UserName/UID not valid.");
                    }
                    else
                    { //user exist is student table, not is ASPNET User, create an entry in aspnet user
                        var gsuser = CreateUser(student);
                        if (gsuser != null)
                            user = gsuser;
                    }
                    mobilenumber = student.numMobileNoForSms;
                }
                else
                {//send otp to user mobile
                    if (user.studentid == "0")
                    {
                        //if admin
                        var student = GetStudent(model.Mobile, dbcodename.MainDatabasesModelid.ToString());
                        if (student == null)
                        { // user not exist in our records
                            return BadRequest("UserName/UID not valid.");
                        }
                    }
                    mobilenumber = user.UserName;
                }
                if(user==null)
                    return BadRequest("UserName/UID not valid.");

                var localotp = string.Empty;

                if (dbcodename.IUIDCode == "DEMO")
                    localotp = "11111";


                var otp =SendSMSOTP(mobilenumber, localotp, dbcodename.MainDatabasesModelid.ToString());
                user.anykey = otp;
                var resultupdateotp = UserManager.Update(user);

                return Ok();

                //var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Token";

                //var responseresult = GetResponseAsDictionary(url, model.Mobile, model.Mobile, dbcodename);

                //if (responseresult.StatusCode == HttpStatusCode.BadRequest)
                //{
                //    return BadRequest("UserName/Password/UID not valid.");
                //}

                //var result = responseresult.Content.ReadAsStringAsync().Result;
                //var result2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                //result2.Add("dbcodeid", dbcodeguid);
                //return Ok(result2);
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.ToString());

                return BadRequest("Some error occured, please contact admin.");
            }
            //return Ok();
        }

        // api/appdetail/OTPLogin
        [AllowAnonymous]
        [Route("OTPLogin")]
        public async Task<IHttpActionResult> OTPLogin(OTPLoginAppModel model)
        {
            try
            {
                if (model == null)
                    return null;
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                var dbcode = model.dbcode;
                var dbcodename = GetDatabasesResult(dbcode);

                if (dbcodename == null)
                {
                    return BadRequest("UserName/UID not valid.");
                }

                var usermanagerlocal = UserManagerMethod(dbcodename);
                var user = await UserManager.FindByNameAsync(model.Mobile);
                if (user.anykey == model.OTP)
                {
                    user.anykey = null;
                    var resultupdateotp0 = UserManager.Update(user);
                    var currenturl = ConfigurationManager.AppSettings["CurrentWebsite"];

                    var url = currenturl + "/Token";
                    var responseresult = GetResponseAsDictionary(url, model.Mobile, dbcodename,user);

                    if (responseresult.StatusCode == HttpStatusCode.BadRequest)
                    {
                        return BadRequest("UserName/Password/UID not valid.");
                    }

                    var result = responseresult.Content.ReadAsStringAsync().Result;
                    var result2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    result2.Add("dbcodeid", dbcodename.MainDatabasesModelid.ToString());
                    result2.Add("usermobile", model.Mobile);

                    return Ok(result2);
                }
                
                    
               
                //user.anykey = null;
                //var resultupdateotp = UserManager.Update(user);
                return BadRequest("OTP is not valid.");
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Some error occured, please contact admin.");
            }
        }

        [AllowAnonymous]
        [Route("ResendOTP")]
        public async Task<IHttpActionResult> ResendOTP(LoginAppModel model)
        {
            try
            {
                if (model == null)
                    return null;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbcode = model.dbcode;
                var dbcodename = GetDatabasesResult(dbcode);

                if (dbcodename == null)
                {
                    return BadRequest("Mobile is not valid. Please try again.");
                }

                var usermanagerlocal = UserManagerMethod(dbcodename);
                var user = await UserManager.FindByNameAsync(model.Mobile);

                var mobilenumber = string.Empty;

                if (user == null)
                {
                    return BadRequest("Mobile is not valid. Please try again.");
                }
                mobilenumber = user.UserName;
                var otp = SendSMSOTP(mobilenumber, user.anykey, dbcodename.MainDatabasesModelid.ToString());
                //user.anykey = otp;
                //var resultupdateotp = UserManager.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest("Some error occured, please contact admin.");
            }
            //return Ok();
        }

        private StudentModel GetStudent(string mobile, string dbcode)
        {
            var studentlist = _studentobj.GetAllByMobile(mobile, dbcode);
            var student = new StudentModel();
            if (studentlist != null)
            {
                student = studentlist.FirstOrDefault();
            }

            return student;
        }

        #endregion

        #region API's

        [Route("GetSchoolDetail")]
        public HttpResponseMessage GetSchoolDetail()
        {
            var webmanager = _schoolobj.GetAll(GetDataBaseCode());
            if (webmanager != null)
            {
                var firstrecord = webmanager.FirstOrDefault();
                var deserializedProduct = JSONGS<SchoolModel>(firstrecord);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

       
        //[Authorize]
        [Route("GetAllChilds")]
        public HttpResponseMessage GetAllChilds()
        {
            var mobile = User.Identity.Name;
            var webmanager = _studentobj.GetAllByMobile(mobile,GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<StudentModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetLedger")]
        public HttpResponseMessage GetLedger(string id) // Total
        {
            //var studentid = GetStudentID();
          var  studentid = Convert.ToInt32(id);

            var webmanager = _LedgerSumaryobj.GetById(studentid, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<LedgerSumaryModel>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetLedgerDetail")]
        public HttpResponseMessage GetLedgerDetail(string id) //individual
        {
            //var studentid = GetStudentID();
           var studentid = Convert.ToInt32(id);

            var webmanager = _LedgerDetailobj.GetAllByStudent(studentid, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<LedgerDetailModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAttendanceDetail")]
        public HttpResponseMessage GetAttendanceDetail(string id)
        {
            //var studentid = GetStudentID();
          var  studentid =Convert.ToInt32(id);
            var webmanager = _Attendanceobj.GetAllByStudent(studentid, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<AttendanceModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAcademicDetail")]
        public HttpResponseMessage GetAcademicDetail(string id)
        {
            //var studentid = GetStudentID();
           var studentid = Convert.ToInt32(id);

            var webmanager = _Academicobj.GetAllByStudent(studentid, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<AcademicModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAcademicSummray")]
        public HttpResponseMessage GetAcademicSummray(string  stuid,string termid,string subjectid)
        {
            var idconditions = new AcademicDetailConditionsModel();
            idconditions.StudentModelID =Convert.ToInt32(stuid);
            idconditions.TermModelid = Convert.ToInt32(termid);
            idconditions.SubjectModelid = Convert.ToInt32(subjectid);

            var webmanager = _AcademicDetailobj.GetAllByConditions(idconditions, GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<AcademicDetailModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetNotificationDetail")]
        public HttpResponseMessage GetNotificationDetail(string id,string type)
        {
            var studentid = Convert.ToInt32(id);

            var studentdetail = _studentobj.GetById(studentid, GetDataBaseCode());

            ClassSectionModel clssec = new ClassSectionModel() { ClassModelid = studentdetail.ClassModelid, SectionModelid = studentdetail.SectionModelid.Value };

            var webmanager = _iNotificationScheduleService.GetForStudentByClassSection(clssec, type, GetDataBaseCode());
            if (webmanager != null)
            {

                var deserializedProduct = JSONGS<IEnumerable<NotificationScheduleModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetAlbums")]
        public HttpResponseMessage GetAlbums()
        {
            var webmanager = _albumobj.GetAll(GetDataBaseCode());
            if (webmanager != null)
            {
                var deserializedProduct = JSONGS<IEnumerable<AlbumModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Route("GetGallery")]
        public HttpResponseMessage GetGallery(string id)
        {
            var webmanager = _imggalleryobj.GetAllByAlbumId(new Guid(id), GetDataBaseCode());
            if (webmanager != null)
            {
                webmanager = webmanager.OrderByDescending(d => d.createdate);
                var deserializedProduct = JSONGS<IEnumerable<ImageGalleryModel>>(webmanager);
                return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }


        ////[Authorize(Roles = "Admin")]
        //[Route("GetAllClasses")]
        //public HttpResponseMessage GetAllClasses()
        //{
        //    var webmanager = _classbj.GetAllClass(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<ClassModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        ////[Authorize(Roles = "Admin")]
        //[Route("GetAllSections")]
        //public HttpResponseMessage GetAllSections()
        //{
        //    var webmanager = _classbj.GetAllSection(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SectionModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        ////[Authorize(Roles = "Admin")]
        //[Route("GetAllSubjects")]
        //public HttpResponseMessage GetAllSubjects()
        //{
        //    var webmanager = _classbj.GetAllSubject(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SubjectModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("GetTeacherClasses")]
        //public HttpResponseMessage GetTeacherClasses(string id)
        //{
        //    var webmanager = _classbj.GetAllClass(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<ClassModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        //[Route("GetTeacherSections")]
        //public HttpResponseMessage GetTeacherSections(string id)
        //{
        //    var webmanager = _classbj.GetAllSection(GetDataBaseCode());

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
        //    var webmanager = _classbj.GetAllSubject(GetDataBaseCode());

        //    if (webmanager != null)
        //    {
        //        var deserializedProduct = JSONGS<IEnumerable<SubjectModel>>(webmanager);
        //        return Request.CreateResponse(HttpStatusCode.OK, deserializedProduct);
        //    }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        //}

        #endregion

        #region Private User
        private ApplicationUser CreateUser(StudentModel model) {
            var nuser = new ApplicationUser()
            {
                UserName = model.numMobileNoForSms,
                PhoneNumber = model.numMobileNoForSms,
                //Email = model.,
                nameofuser = model.strFatherName,
                remarks = "Parent id",
                parentid = new Guid(),
                studentid=model.StudentModelID.ToString(),
            };

            var result = UserManager.Create(nuser, "PA@#$VPF1df$%P");
            if (!result.Succeeded)
                return null;
            //var rolename = "Parent";
            //var role = RoleManager.FindByName(rolename);
            //if (role == null)
            //{
            //    role = new IdentityRole(rolename);
            //    var roleresult = RoleManager.Create(role);
            //}
            //var nresult = UserManager.AddToRole(nuser.Id, role.Name);
            setParentRole(nuser.Id);

            return nuser;
        }

        private void setParentRole(string id)
        {
            var rolename = "Parent";
            var role = RoleManager.FindByName(rolename);
            if (role == null)
            {
                role = new IdentityRole(rolename);
                var roleresult = RoleManager.Create(role);
            }
            var nresult = UserManager.AddToRole(id, role.Name);
        }

        private string SendSMSOTP(string mobilenumber,string otp, string dbcodeid)
        {
            if(otp==string.Empty)
                otp = StaticData.RandomNumber(4);
            if (otp == "11111") //if demo
            {
                return otp;
            }

            
            var smsmodelmain = _settingsobj.GetSettingByType(SettingsType.SMS.ToString(), dbcodeid);
            var smsmodel = JSONGSTRING<SMSModel>(smsmodelmain.SettingsContent);
            var smssent = StaticData.sendsmsany("OTP for login to eAcademics is " + otp, mobilenumber, smsmodel);

            return otp;
        }

       

   //     private HttpResponseMessage GetResponseAsDictionary(string host,
   //string userName,  string dbcodename)
   //     {
   //         HttpClient client = new HttpClient();
   //         var pairs = new List<KeyValuePair<string, string>>
   //             {
   //                 new KeyValuePair<string, string>( "grant_type", "password" ),
   //                 new KeyValuePair<string, string>( "username", userName ),
   //                 new KeyValuePair<string, string> ( "Password", "PA@#$VPF1df$%P" ),
   //                 new KeyValuePair<string, string> ( "dbcodename", dbcodename )

   //             };
   //         var content = new FormUrlEncodedContent(pairs);

   //         // Attempt to get a token from the token endpoint of the Web Api host:
   //         HttpResponseMessage response =
   //             client.PostAsync(host, content).Result;

   //         return response;

   //     }

        private HttpResponseMessage GetResponseAsDictionary(string host,
   string userName, MainDatabasesModel dbcodename, ApplicationUser user)
        {
            HttpClient client = new HttpClient();
            var password = ConfigurationManager.AppSettings["ParentPassword"];
            var adminid = string.Empty;
            if (user.studentid == "0")
            {
                adminid = user.Id;
                setParentRole(adminid);
            }


            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password ),
                    new KeyValuePair<string, string> ( "IDataSource", dbcodename.IDataSource ),
                    new KeyValuePair<string, string> ( "IInitialCatalog", dbcodename.IInitialCatalog ),
                    new KeyValuePair<string, string> ( "IUsername", dbcodename.IUsername ),
                    new KeyValuePair<string, string> ( "IPassword", dbcodename.IPassword ),
                    new KeyValuePair<string, string> ( "adminid", adminid ),
                };
            var content = new FormUrlEncodedContent(pairs);

            // Attempt to get a token from the token endpoint of the Web Api host:
            HttpResponseMessage response =
                client.PostAsync(host, content).Result;

            return response;
        }

        private MainDatabasesModel GetDatabasesResult(string dbcode)
        {
            dbcode = dbcode.ToUpper().Trim();
            var dbresult = _MainDatabasesService.GetByName(dbcode, string.Empty);
            if (dbresult != null)
                return dbresult;
            return null;
        }
        #endregion
    }
}
