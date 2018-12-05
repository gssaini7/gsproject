
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace R.BusinessEntities
{
    // Models used as parameters to Admins by GS.

    public class MainDatabasesModel : EntityCommonModel
    {
        #region properties

        public Guid MainDatabasesModelid { get; set; }
        public string IDataSource { get; set; }
        public string IInitialCatalog { get; set; }
        public string IUsername { get; set; }
        public string IPassword { get; set; }
        public string IName { get; set; }
        public string IUIDCode { get; set; }

        #endregion
    }

    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
    //    {

    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
    //        // Add custom user claims here
    //        userIdentity.AddClaim(new Claim("studentid", this.studentid));
    //        userIdentity.AddClaim(new Claim("nameofuser", this.nameofuser));

    //        return userIdentity;

    //    }

    //    public string nameofuser { get; set; }
    //    public string remarks { get; set; }
    //    public Guid parentid { get; set; }
    //    public Guid uprkey { get; set; } //unique password recovery key
    //    public string anykey { get; set; } //any key string for otp etc.
    //    public string studentid { get; set; }
    //}


}
