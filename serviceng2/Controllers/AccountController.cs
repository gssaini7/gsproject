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
using System.Configuration;
using System.IO;
using System.Collections.Specialized;
using R.BAL;

namespace USoftEducation.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize(Roles = "Legitimate")]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private iStudentService _studentService;




        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, iStudentService studentservice )
        {
            UserManager = userManager;
            RoleManager = roleManager;
            AccessTokenFormat = accessTokenFormat;
            this._studentService = studentservice;
        }

      

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

                //if (_userManager == null)
                //{
                //    var code = HttpContext.Current.Session["dbcode"];
                //    //var code = Request.Form.Get("SchoolCode");//Get from FORM\QueryString\Session whatever you wants
                //    if (code != null)
                //    {
                //        var appDbContext = new ApplicationDbContext(code.ToString());

                //        Request.GetOwinContext().Set<ApplicationDbContext>(appDbContext);
                //        Request.GetOwinContext().Set<ApplicationUserManager>(new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext))); //OR USE your specified create Method
                //    }
                //    _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //}
                //return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationUserManager UserManagerMethod(string dbcode)
        {
            if (_userManager == null)
            {
                var code = dbcode; 
                //var code = Request.Form.Get("SchoolCode");//Get from FORM\QueryString\Session whatever you wants
                if (code != null || code != string.Empty)
                {
                    var appDbContext = new ApplicationDbContext(code.ToString());

                    Request.GetOwinContext().Set<ApplicationDbContext>(appDbContext);
                    Request.GetOwinContext().Set<ApplicationUserManager>(new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext))); //OR USE your specified create Method
                }
                _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            return _userManager;
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


        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        //[AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (model == null)
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser() { UserName = model.Mobile, PhoneNumber = model.Mobile, Email = model.Email, nameofuser=model.nameofuser, remarks=model.remarks };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            var role = RoleManager.FindByName(model.UserRole);
            if (role == null)
            {
                role = new IdentityRole(model.UserRole);
                var roleresult = RoleManager.Create(role);
            }
            var nresult = UserManager.AddToRole(user.Id, role.Name);
            return Ok();
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
                //if ((HttpContext.Current.Session["SomeData"] as string) == null)
                //    HttpContext.Current.Session["SomeData"] = "Hello from session";

                //var ar = HttpContext.Current.Session["SomeData"].ToString();
                ////Request. Form.Get("SchoolCode");
                //if ((HttpContext.Current.Session["dbcode"] as string) == null)
                //    HttpContext.Current.Session["dbcode"] = "Hello from session";
                //HttpContext.Current.Session.Contents["dbcode"] = dbcode;

                //var data = new LoginTokenWebModel{
                //    grant_type= "password",
                //Mobile= model.Mobile,
                //password= model.Password,
                //    dbcode="1234",
                //};

                var dbcodeguid = ConfigurationManager.AppSettings["dbcode_" + dbcode];
                var dbcodename = ConfigurationManager.AppSettings[dbcodeguid];

                if (dbcodename == null) {
                    return BadRequest("UserName/Password/UID not valid.");
                }

                var usermanagerlocal = UserManagerMethod(dbcodename);
                var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Token";

                var responseresult = GetResponseAsDictionary(url, model.Mobile, model.Password, dbcodename);

                if(responseresult.StatusCode== HttpStatusCode.BadRequest)
                {
                    return BadRequest("UserName/Password/UID not valid.");
                }

                var result = responseresult.Content.ReadAsStringAsync().Result;
                var result2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                result2.Add("dbcodeid", dbcodeguid);
                return Ok(result2);

                //var httpRequest = HttpWebRequest.Create(url);
                //httpRequest.Method = "POST";
                ////httpRequest.ContentType = "application/x-www-form-urlencoded";


                //var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                //streamWriter.Write(data);
                //streamWriter.Close();

                //var result = httpRequest.GetResponse();
                //using (var wb = new WebClient())
                //{
                //    var data2 = new NameValueCollection();
                //    data2["grant_type"] = "password";
                //    data2["UserName"] = model.Mobile;
                //    data2["password"] = model.Password;



                //    var response = wb.UploadValues(url, "POST", data2);
                //    string responseInString = Encoding.UTF8.GetString(response);
                //}
            }
            catch(Exception ex) {
                return BadRequest("Some error occured, please contact admin.");
            }
            //return Ok();
        }

        private HttpResponseMessage GetResponseAsDictionary(string host,
    string userName, string password, string dbcodename)
        {
            HttpClient client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password ),
                    new KeyValuePair<string, string> ( "dbcodename", dbcodename )

                };
            var content = new FormUrlEncodedContent(pairs);

            // Attempt to get a token from the token endpoint of the Web Api host:
            HttpResponseMessage response =
                client.PostAsync(host, content).Result;

            return response;

            //if (response.StatusCode == HttpStatusCode.BadRequest) {
            //    return null;
            //}

            //var result = response.Content.ReadAsStringAsync().Result;
            //// De-Serialize into a dictionary and return:
            //Dictionary<string, string> tokenDictionary =
            //    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            //return tokenDictionary;
        }

        //private R.BusinessEntities.MainDatabasesModel GetDatabasesResult(string dbcode) {
        //    this._MainDatabasesService = new MainDatabasesService(;
        //    var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Token";
        //    var responseresult = GetResponseAsDictionary(url, model.Mobile, model.Password, dbcodename);
        //    return null;
        //}

        //[Authorize]
        //[Route("UpdateUser")]
        //public async Task<IHttpActionResult> UpdateUser(UserDetailModel model)
        //{
        //    if (model.Password == null)
        //    {
        //        ModelState.Remove("model.Password");
        //        ModelState.Remove("model.ConfirmPassword");
        //    }

        //    if (model == null)
        //        return null;
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user =await UserManager.FindByIdAsync(model.UserID);
        //    user.Email = model.Email;
        //    user.nameofuser = model.nameofuser;
        //    user.PhoneNumber = model.Mobile;
        //    user.remarks = model.remarks;
        //    user.UserName = model.Mobile;


        //    IdentityResult result = await UserManager.UpdateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    if (model.Password != null)
        //    {
        //        IdentityResult resultpswdold = await UserManager.RemovePasswordAsync(user.Id);
        //        IdentityResult resultpswd = await UserManager.AddPasswordAsync(user.Id, model.Password);
        //        if (!resultpswd.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }
        //    }

        //    return Ok();
        //}

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("users")]
        //public HttpResponseMessage users(string rolename)
        //{
        //    try
        //    {
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

        // POST api/Account/GetUserByEmail
        //[AllowAnonymous]
        //[Route("GetUserByEmail")]
        //public async Task<IHttpActionResult> GetUserByEmail(ForgetPasswordModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    IdentityUser user = await UserManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //    //    var callbackUrl = Url.Action("ResetPassword", "Account",
        //    //new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //    var callbackUrl = "ss";
        //    await UserManager.SendEmailAsync(user.Id, "Reset Password",
        //"Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");


        //    //string subjectl = "Reset password";
        //    //StringBuilder sbEmailBodyl = new StringBuilder();
        //    //UserManager.SendEmailAsync()
        //    //sbEmailBodyl.Append("Hi,<br/> Click on below given link to reset your password<br/>");
        //    //sbEmailBodyl.Append("<a href='http://localhost:60300//login/ResetPassword/?user=" + user.Id+ "'>Click here to change your password</a><br/>");

        //    //bool result = StaticData.SendMail(user.Email, subjectl, sbEmailBodyl.ToString());

        //    return Ok();
        //}

        // POST api/Account/ResetPassword
        //[AllowAnonymous]
        //[Route("ResetPassword")]
        //public async Task<IHttpActionResult> ResetPassword(SetPasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.NewPassword,
        //        model.);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
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

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
