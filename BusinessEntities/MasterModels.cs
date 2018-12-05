using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace R.BusinessEntities
{

    public class LoginModel
    {
        [Required(ErrorMessage="Enter Mobile Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid 10 digit mobile number")]
        public string MobileLogin { get; set; }

        public string Password { get; set; }
    }

    public class OTPModel
    {
        [Required(ErrorMessage="Enter OTP")]
        public string Otpvalue { get; set; }
    }



    public class updatepassword :ClientRecordModel
    {
        [Required]
        [Display(Name = "New Password")]
        public string newpassword { get; set; }
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.CompareAttribute("newpassword", ErrorMessage = "Passwords does not match.")]
        public string confirmpassword { get; set; }

    }
    public class changepassword : updatepassword
    {
        [Display(Name = "Old Password")]
        public string oldpassword { get; set; }
    }
    public class CategoryMasterModel
    {
        public int catid { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string catname { get; set; }
    }

    public partial class TitleMasterModel
    {
        public int titleid { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string titlename { get; set; }
    }

    public class ClientRecordModel
    {
        public int recordid { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string productname { get; set; }
        [Required]
        [Display(Name = "Product ID")]
        public string productid { get; set; }
        [Required]
        [Display(Name = "Business Name")]
        public string businessname { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string clientname { get; set; }
        [Required]
        [Display(Name = "Client Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string clientemail { get; set; }

        [Required]
        [Display(Name = "Client Mobile")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid 10 digit mobile number")]
        //[Remote("MobileExists", "user", HttpMethod = "POST", ErrorMessage = "User with this Mobile already exists")]
        public string contactmobile { get; set; }

        [Required]
        [Display(Name = "Client Address")]
        public string clientaddress { get; set; }
        public string formodule { get; set; }
        public string userrole { get; set; }
        public bool userblocked { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> expirydate { get; set; }

        public string upassword { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }



    }

    public class DescriptionModuleModel
    {
        public int descriptionid { get; set; }
        [Required]
        [Display(Name = "Title")]
        public Nullable<int> desctitile { get; set; }
        [Required]
        [Display(Name = "Category")]
        public Nullable<int> desccategory { get; set; }
        [Required]
        [Display(Name = "Video Link")]
        public string descvideolink { get; set; }
        public string websitemodule { get; set; }
        public string descanydescription { get; set; }

        public virtual CategoryMasterModel Category { get; set; }
        public virtual TitleMasterModel Title { get; set; }
    }

    public class CouponEntity { 
        public int couponid { get; set; }
        [Required]
        [Display(Name = "Coupon Code")]
        public string couponcode { get; set; }
        [Display(Name = "Message for User")]

        public string cmsgforuser { get; set; }
        [Display(Name = "Admin Remarks")]

        public string cAdminRemarks { get; set; }
        [Display(Name = "Blocked Coupon")]

        public bool cblocked { get; set; }
        [Required]
        [Display(Name = "Product Amount")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        public int camount { get; set; }

    }

    public class ProjectDetailEntity
    {
        public int amtdtid { get; set; }
        public string projectdetail { get; set; }
        public string prowtype { get; set; }
    }

    public class ProjectDetailEntityModel
    {
        public int amtdtid { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public string projectname { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string projecttype { get; set; }
        [Required]
        [Display(Name = " Cost")]
        public int projectcost { get; set; }
        [Required]
        [Display(Name = "Amount Advance")]
        public int projectadvance { get; set; }
        [Required]
        [Display(Name = "Amount Pending")]
        public int projectpending { get; set; }
        [Required]
        [Display(Name = "Deliverables")]
        public string projectdeliverables { get; set; }
        [Required]
        [Display(Name = "Deliverables Pending")]
        public string projectdeliverablespending { get; set; }

        [Display(Name = "Deliverables Completed")]
        public string projectdeliverablescompleted { get; set; }
    }
   

}
