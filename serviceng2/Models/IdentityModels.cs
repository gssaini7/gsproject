using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Data.SqlClient;
using R.BusinessEntities;

namespace USoftEducation.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {

            //Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
           var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            //Add custom user claims here
            userIdentity.AddClaim(new Claim("studentid", this.studentid));
            userIdentity.AddClaim(new Claim("nameofuser", this.nameofuser));

            return userIdentity;

        }

        public string nameofuser { get; set; }
        public string remarks { get; set; }
        public Guid parentid { get; set; }
        public Guid uprkey { get; set; } //unique password recovery key
        public string anykey { get; set; } //any key string for otp etc.
        public string studentid { get; set; }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("ussdbEntities", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(string connstr)
           : base(ConnectionString(connstr), throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(R.BusinessEntities.MainDatabasesModel connstrmodel)
          : base(ConnectionString(connstrmodel), throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

        private static string ConnectionString(string dbname)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = ".";
            sqlBuilder.InitialCatalog = dbname;
            sqlBuilder.PersistSecurityInfo = true;
            sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.MultipleActiveResultSets = true;
            return sqlBuilder.ToString();
        }

        private static string ConnectionString(R.BusinessEntities.MainDatabasesModel dbname)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dbname.IDataSource;
            sqlBuilder.InitialCatalog = dbname.IInitialCatalog;
            if (dbname.IUsername == null || dbname.IUsername == string.Empty)
            {
                sqlBuilder.PersistSecurityInfo = true;
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilder.UserID = dbname.IUsername;
                sqlBuilder.Password = dbname.IPassword;
            }

            //sqlBuilder.PersistSecurityInfo = true;
            //sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.MultipleActiveResultSets = true;
            return sqlBuilder.ToString();
        }

    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext(string connectionstring)
    //        : base(connectionstring, throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}