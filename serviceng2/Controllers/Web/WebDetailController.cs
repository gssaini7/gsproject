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
    [RoutePrefix("api/webdetail")]
    public class WebDetailController : BaseAPIController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        iMainDatabasesService _MainDatabasesService;

        public WebDetailController(iMainDatabasesService iMainDatabasesService)
        {
            this._MainDatabasesService = iMainDatabasesService;
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

        #endregion

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
                //return BadRequest(ex.ToString());

                return BadRequest("Some error occured, please contact admin.");
            }
            //return Ok();
        }

        private MainDatabasesModel GetDatabasesResult(string dbcode)
        {
            dbcode = dbcode.ToUpper().Trim();
            var dbresult = _MainDatabasesService.GetByName(dbcode, string.Empty);
            if (dbresult != null)
                return dbresult;
            return null;
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


        // POST api/webdetail/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbcodename = GetDatabasesResult(GetDataBaseCode());
            var usermanagerlocal = UserManagerMethod(dbcodename);

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
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



    }
}
