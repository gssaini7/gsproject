using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using USoftEducation.Models;
using USoftEducation.Providers;
using USoftEducation.Results;
using R.BusinessEntities;
using System.Web.Http.Cors;
using System.Net;
using gsproject;
using System.Text;
using System.Linq;
using System.Configuration;
using R.BAL;

namespace USoftEducation.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        iMainDatabasesService _MainDatabasesService;

        public AdminController(iMainDatabasesService iMainDatabasesService)
        {
            this._MainDatabasesService = iMainDatabasesService;
        }

        //public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager,
        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        //{
        //    UserManager = userManager;
        //    RoleManager = roleManager;
        //}

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

        private ApplicationRoleManager RoleManagerMethod(MainDatabasesModel dbcode)
        {
            if (_roleManager == null)
            {
                var code = dbcode;
                if (code != null )
                {
                    var appDbContext = new ApplicationDbContext(code);

                    Request.GetOwinContext().Set<ApplicationDbContext>(appDbContext);
                    Request.GetOwinContext().Set<ApplicationRoleManager>(new ApplicationRoleManager(new RoleStore<IdentityRole>(appDbContext))); //OR USE your specified create Method
                }
                _roleManager = Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            return _roleManager;
        }

        [Authorize(Roles ="Admin")]
        [Route("Create")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDetail(RegisterBindingModel model)
        {
            try
            {
                if (model == null)
                    return null;

                ModelState.Remove("model.Password");
                ModelState.Remove("model.ConfirmPassword");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (model.parentid == null)
                    model.parentid = new Guid(User.Identity.GetUserId());

                var user = new ApplicationUser() { UserName = model.Mobile, PhoneNumber = model.Mobile, Email = model.Email,
                    nameofuser=model.nameofuser, remarks = model.remarks, parentid=model.parentid, studentid="0" };

                SetDataBaseDynamic();
                IdentityResult result = await UserManager.CreateAsync(user, "A@#$VF1df$%");
                if (!result.Succeeded)
                {
                    if (CheckUserIsParent(user, model.parentid))
                        return Ok();
                    //var nuser = await UserManager.FindByNameAsync(user.UserName);
                    //var parentroleid = await RoleManager.FindByNameAsync("Parent");
                    //var isPresent = false;
                    //foreach (var role in nuser.Roles)
                    //{
                    //    if (role.RoleId == parentroleid.Id)
                    //    {
                    //        isPresent = true;
                    //        break;
                    //    }
                    //}
                    //if (CheckUserIsParent(user))
                    //{
                    //    nuser.parentid = model.parentid;
                    //    var nres = await UserManager.UpdateAsync(nuser);
                    //    return Ok("There is already a parent with "+nuser.UserName+" number, record created."); //not shown
                    //}
                    return GetErrorResult(result);
                }
            }
            catch(Exception e) {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        private bool CheckUserIsParent(ApplicationUser checkuser,Guid parentid)
        {
            var nuser =  UserManager.FindByName(checkuser.UserName);
            var nuserroles = UserManager.GetRoles(nuser.Id);
            var isPresent = false;
            foreach (var role in nuserroles)
            {
                if (role == "Parent")
                {
                    isPresent = true;
                }
                else {
                    UserManager.RemoveFromRole(nuser.Id, role);
                }
            }
            if (isPresent)
            {
                nuser.parentid = parentid;
                var nres =  UserManager.Update(nuser);
            }

            return isPresent;
        }

        [Authorize(Roles ="Admin")]
        [Route("UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser(UserDetailModel model)
        {
            if (model.Password == null)
            {
                ModelState.Remove("model.Password");
                ModelState.Remove("model.ConfirmPassword");
            }

            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SetDataBaseDynamic();

            var user =await UserManager.FindByIdAsync(model.UserID);
            user.Email = model.Email;
            user.nameofuser = model.nameofuser;
            user.PhoneNumber = model.Mobile;
            user.remarks = model.remarks;
            user.UserName = model.Mobile;

            
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            if (model.Password != null)
            {
                IdentityResult resultpswdold = await UserManager.RemovePasswordAsync(user.Id);
                IdentityResult resultpswd = await UserManager.AddPasswordAsync(user.Id, model.Password);
                if (!resultpswd.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteAdmin(UserDetailModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest("An error occured, please try again.");

                SetDataBaseDynamic();

                var user = await UserManager.FindByIdAsync(model.UserID);

                var currentuserid = new Guid(User.Identity.GetUserId());

                if (currentuserid == new Guid(user.Id)) {
                    return BadRequest("You cannot delete yourself.");

                }

                if (user != null) {
                    GetChildUsers(user);
                    //var childuser = UserManager.Users.Where(p => p.parentid == new Guid(user.Id)).ToList();
                    //if (childuser.Any()) {

                    //}
                }

                //ModelState.Remove("model.Password");
                //ModelState.Remove("model.ConfirmPassword");

                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}

                //if (model.parentid == null)
                //    model.parentid = new Guid(User.Identity.GetUserId());

                //var user = new ApplicationUser()
                //{
                //    UserName = model.Mobile,
                //    PhoneNumber = model.Mobile,
                //    Email = model.Email,
                //    nameofuser = model.nameofuser,
                //    remarks = model.remarks,
                //    parentid = model.parentid
                //};

                //SetDataBaseDynamic();
                //IdentityResult result = await UserManager.CreateAsync(user, "A@#$VF1df$%");
                //if (!result.Succeeded)
                //{
                //    return GetErrorResult(result);
                //}
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "An error occured please contact administrator.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        private ApplicationUser GetChildUsers(ApplicationUser parentuser)
        {
            var childuser = UserManager.Users.Where(p => p.parentid == new Guid(parentuser.Id)).ToList();
            if (childuser.Any())
            {
                foreach (var ch in childuser) {
                    var nchuser = GetChildUsers(ch);
                }
            }
            DeleteAdmin(parentuser);
            return null;
        }

        private IdentityResult DeleteAdmin(ApplicationUser admin)
        {
            if (CheckUserIsParent(admin,new Guid())) //if admin is parent do not delete, update parentid
            {
                return null;
            }
            IdentityResult result =  UserManager.Delete(admin);
            return result;
        }

        //[Authorize(Roles = "SuperAdmin")]
        //[HttpGet]
        //[Route("users")]
        //public HttpResponseMessage users(string rolename)
        //{
        //    try
        //    {
        //        SetDataBaseDynamic();
        //        var role = RoleManager.FindByName(rolename);

        //        var managers = role.Users;
        //        var users = new List<UserDetailModel>();

        //        foreach (var user in managers)
        //        {
        //            var suser = UserManager.FindById(user.UserId);
        //            var nuser = new UserDetailModel();
        //            nuser.UserID = suser.Id;
        //            nuser.Email = suser.Email;
        //            nuser.Mobile = suser.PhoneNumber;
        //            nuser.UserRole = rolename;
        //            nuser.nameofuser = suser.nameofuser;
        //            nuser.remarks = suser.remarks;
        //            users.Add(nuser);
        //        }

        //        if (users != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, users);
        //        }

        //    }
        //    catch { }
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");

        //}


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        [Route("Admins")]
        public HttpResponseMessage AllByParent()
        {
            try
            {
                var currentuserid = new Guid(User.Identity.GetUserId());
                var users =new List<UserDetailAdminModel>();
                SetDataBaseDynamic();

                var parentuser = UserManager.FindById(User.Identity.GetUserId());
                var parentuserfinal = appuser_to_detailuser(parentuser);
                parentuserfinal.AdminChildren=getUsersfromdb(currentuserid);
                users.Add(parentuserfinal);
              
                if (users != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, users);
                }

            }
            catch { }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");

        }

        private List<UserDetailAdminModel> getUsersfromdb(Guid parentid)
        {
            SetDataBaseDynamic();

            var dbusers = UserManager.Users.Where(p => p.parentid == parentid);
            var users = new List<UserDetailAdminModel>();
            foreach (var user in dbusers)
            {
              
                var nuser = appuser_to_detailuser(user);
                var nu = getUsersfromdb(new Guid(user.Id));
                if (nu.Count >= 1)
                {
                    nuser.AdminChildren = nu;
                }
                users.Add(nuser);
            }
            return users;
        }

        private UserDetailAdminModel appuser_to_detailuser(ApplicationUser appuser)
        {
            var nuser = new UserDetailAdminModel();
            nuser.UserID = appuser.Id;
            nuser.Email = appuser.Email;
            nuser.Mobile = appuser.PhoneNumber;
            nuser.nameofuser = appuser.nameofuser;
            nuser.remarks = appuser.remarks;
            nuser.parentid = appuser.parentid;
            return nuser;
        }

        [AllowAnonymous]
        [Route("WebLogin")]
        public async Task<IHttpActionResult> WebLogin(LoginWebModel model)
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
                    return BadRequest("UserName/Password/UID not valid.");
                }

                var usermanagerlocal = UserManagerMethod(dbcodename);
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Token";

                var responseresult = GetResponseAsDictionary(url, model.Mobile, model.Password, dbcodename);

                if (responseresult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest("UserName/Password/UID not valid.");
                }

                var result = responseresult.Content.ReadAsStringAsync().Result;
                var result2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                result2.Add("dbcodeid", dbcodename.MainDatabasesModelid.ToString());
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return BadRequest("Some error occured, please contact admin.");
            }
            //return Ok();
        }

       

        private HttpResponseMessage GetResponseAsDictionary(string host,
    string userName, string password, MainDatabasesModel dbcodename)
        {
            HttpClient client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password ),
                    new KeyValuePair<string, string> ( "IDataSource", dbcodename.IDataSource ),
                    new KeyValuePair<string, string> ( "IInitialCatalog", dbcodename.IInitialCatalog ),
                    new KeyValuePair<string, string> ( "IUsername", dbcodename.IUsername ),
                    new KeyValuePair<string, string> ( "IPassword", dbcodename.IPassword ),
                };
            var content = new FormUrlEncodedContent(pairs);

            // Attempt to get a token from the token endpoint of the Web Api host:
            HttpResponseMessage response =
                client.PostAsync(host, content).Result;

            return response;
        }


        // POST api/Admin/GetUserByEmail
        [AllowAnonymous]
        [Route("GetUserByEmail")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUserByEmail(ForgetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbcode = model.dbcode;
                var dbcodename = GetDatabasesResult(dbcode);
                if (dbcodename == null)
                {
                    return BadRequest("Email/UID not valid.");
                }
                var usermanagerlocal = UserManagerMethod(dbcodename);
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("Email/UID not valid.");
                }
                var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("UsoftApplication");
                UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("PasswordReset"));

                var tokena =  UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var encodedtoken = HttpUtility.UrlEncode(tokena.Result);

                string subjectl = "Reset password";
                StringBuilder sbEmailBodyl = new StringBuilder();
               
                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var uprcode = Guid.NewGuid();
                sbEmailBodyl.Append("<a href='" + baseUrl + "/home/ResetPassword/?user=" + user.Email + "&uid="+dbcode+"&uprcode="+ uprcode+ "&token=" + encodedtoken + "'>Click here to change your password</a><br/>");
                bool result = StaticData.SendMail(user.Email, subjectl, sbEmailBodyl.ToString());

                user.uprkey = uprcode;
                var updateuserresult =await UserManager.UpdateAsync(user);
                
                return Ok();
            }
            catch(Exception Ex) { }
            return BadRequest("An error occured, please contact admin.");

        }



        //POST api/Admin/ResetPassword
       [AllowAnonymous]
       [Route("ResetPassword")]
       [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbcode = model.Uid;
                var dbcodename = GetDatabasesResult(dbcode);
               
                if (dbcodename == null)
                {
                    return BadRequest("Request not valid. Please try again.");
                }
                var usermanagerlocal = UserManagerMethod(dbcodename);

                var user = UserManager.Users.Where(u=>(u.Email==model.Useremail && u.uprkey==new Guid(model.Uprcode))).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("Request not valid. Please try again.");
                }

                var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("UsoftApplication");
                UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("PasswordReset"));

                //return Ok();
                var decodedtoken = HttpUtility.UrlDecode(model.tokencode);
                IdentityResult resultl = await UserManager.ResetPasswordAsync(user.Id, decodedtoken,
                   model.NewPassword);

                //IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.NewPassword,
                //    model.NewPassword);

                if (!resultl.Succeeded)
                {
                    return GetErrorResult(resultl);
                }
            }
            catch (Exception ex) {
                return BadRequest("An error occured, please contact admin.");

            }

            return Ok();
        }

        [Authorize(Roles = "Legitimate")]
        [Route("GetRoles")]
        public HttpResponseMessage GetRoles()
        {
            try
            {
             
                SetDataBaseDynamic();

                var b = RoleManager.FindByName("");
                var roles = RoleManager.Roles;
                var roleslist=new List<IdentityRole>(roles);
                if (roleslist != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, roleslist);
                }

            }
            catch { }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");

        }
        [Authorize]
        [Route("GetCurrentUserRoles")]
        public HttpResponseMessage GetCurrentUserRoles()
        {
            try
            {
                //System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                //string dbcodeid = string.Empty;
                //if (headers.Contains("dbcodeid"))
                //{
                //    dbcodeid = headers.GetValues("dbcodeid").First();
                //}
                //var dbcodename = System.Configuration.ConfigurationManager.AppSettings[dbcodeid];
                //var usermanager_L = UserManagerMethod(dbcodename);
                SetDataBaseDynamic();

                var user = UserManager.FindById(User.Identity.GetUserId());
                var roles = user.Roles;
                if (roles != null)
                {
                    var roleslist = new List<string>();
                    //var r = RoleManagerMethod(dbcodename);
                    foreach (var role in roles)
                    {
                        var roleidentity = RoleManager.FindById(role.RoleId);
                        roleslist.Add(roleidentity.Name);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, roleslist);
                }

            }
            catch { }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

        [Authorize(Roles = "Admin")]
        [Route("GetRolesByID")]
        public HttpResponseMessage GetRolesByIDs(string id) //user id
        {

            try
            {
                SetDataBaseDynamic();

                var user = UserManager.FindById(id);
                var roles = user.Roles;
                if (roles != null)
                {
                    var roleslist = new List<string>();
                    foreach (var role in roles)
                    {
                        var roleidentity = RoleManager.FindById(role.RoleId);
                        roleslist.Add(roleidentity.Name);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, roleslist);
                }

            }
            catch { }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");

        }
        [Authorize(Roles = "Admin")]
        [Route("AssignRole")]
        public async Task<IHttpActionResult> AssignRole(RolesDetailModel model)
        {
            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SetDataBaseDynamic();

            var user =await UserManager.FindByIdAsync(model.UserID);
            if (user != null)
            {
                var roleslist = new List<string>();
                foreach (var role in user.Roles)
                {
                    var roleidentity = RoleManager.FindById(role.RoleId);
                    roleslist.Add(roleidentity.Name);
                }

                if (model.Rolenames.Length > 0)
                {
                    foreach (var rolename in model.Rolenames)
                    {
                        var validrole = await RoleManager.FindByNameAsync(rolename);
                        if (validrole!=null)
                        {
                            var isRoleAlreadyExists = user.Roles.Where(r => r.RoleId == validrole.Id).FirstOrDefault();
                            if (isRoleAlreadyExists == null)
                            {
                                await UserManager.AddToRoleAsync(user.Id, rolename);
                            }
                            else {
                                roleslist.Remove(rolename);
                            }                        
                        }
                    }
                }
                if (roleslist.Count > 0) {
                    foreach (var role in roleslist) {
                        await UserManager.RemoveFromRoleAsync(user.Id, role);
                    }
                }
                
            }
            return Ok();
        }

        [Authorize(Roles = "Legitimate")]
        [Route("CreateRole")]
        public async Task<IHttpActionResult> CreateRole(IdentityRole model)
        {
            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SetDataBaseDynamic();

            var role =await RoleManager.FindByNameAsync(model.Name);
            if (role == null)
            {
                role = new IdentityRole(model.Name);
                var roleresult =await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    return GetErrorResult(roleresult);
                }
                return Ok();
            }
            else {
                ModelState.AddModelError("", "Role already existed.");
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Legitimate")]
        [Route("UpdateRole")]
        public async Task<IHttpActionResult> UpdateRole(IdentityRole model)
        {
            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SetDataBaseDynamic();

            var role =await RoleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                if (role.Name == "DbMain")
                {
                    ModelState.AddModelError("", "You cannot update "+ role.Name);
                    return BadRequest(ModelState);
                }
                var result = await RoleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                return Ok();
            }
            else
            {
                ModelState.AddModelError("", "Role not existed.");
                return BadRequest(ModelState);
            }
        }
        [Authorize(Roles = "Legitimate")]
        [Route("DeleteRole")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteRole(IdentityRole model)
        {
            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SetDataBaseDynamic();

            var role = await RoleManager.FindByNameAsync(model.Name);
            if (role != null)
            {
                if (role.Name == "DbMain")
                {
                    ModelState.AddModelError("", "You cannot delete " + role.Name);
                    return BadRequest(ModelState);
                }
                var roleresult = await RoleManager.DeleteAsync(role);
                if (!roleresult.Succeeded)
                {
                    return GetErrorResult(roleresult);
                }
                return Ok();
            }
            else
            {
                ModelState.AddModelError("", "Role not existed.");
                return BadRequest(ModelState);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            if (disposing && _roleManager != null)
            {
                _roleManager.Dispose();
                _roleManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private MainDatabasesModel GetDatabasesResult(string dbcode)
        {
            dbcode = dbcode.ToUpper().Trim();
            var dbresult = _MainDatabasesService.GetByName(dbcode, string.Empty);
            if (dbresult != null)
                return dbresult;
            return null;
        }

        private MainDatabasesModel GetDatabasesResultByID(string dbcideid)
        {
            var dbresult = _MainDatabasesService.GetById(new Guid(dbcideid), string.Empty);
            if (dbresult != null)
                return dbresult;
            return null;
        }

        private void SetDataBaseDynamic() {

            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            string dbcodeid = string.Empty;
            if (headers.Contains("dbcodeid"))
            {
                dbcodeid = headers.GetValues("dbcodeid").First();
            }
            //var dbcodename = System.Configuration.ConfigurationManager.AppSettings[dbcodeid];
            var dbcredresult = GetDatabasesResultByID(dbcodeid);
            var usermanager_L = UserManagerMethod(dbcredresult);
            var r = RoleManagerMethod(dbcredresult);
        }

       

        private IHttpActionResult GetErrorResult(IdentityResult result)
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

      
        #endregion
    }
}
