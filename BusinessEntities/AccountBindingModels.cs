using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Newtonsoft.Json;

namespace R.BusinessEntities
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        //[Required]
        [Display(Name = "LoginUserName")]
        public string LoginUserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public string UserRole { get; set; }

        public string nameofuser { get; set; }
        public string remarks { get; set; }

        public Guid parentid { get; set; }


    }





    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ForgetPasswordModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UID")]
        public string dbcode { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Useremail { get; set; }

        [Required]
        public string Uid { get; set; }

        [Required]
        public string Uprcode { get; set; }

        [Required]
        public string tokencode { get; set; }

    }

    public class UserDetailModel : RegisterBindingModel
    {
        public string UserID { get; set; }
        public bool isParent { get; set; }


        //public string Email { get; set; }
        //public string Mobile { get; set; }
        //public string UserRole { get; set; }
        //public string nameofuser { get; set; }
        //public string remarks { get; set; }
    }

    public class UserDetailAdminModel : RegisterBindingModel
    {
        public string UserID { get; set; }
       
        public ICollection<UserDetailAdminModel> AdminChildren { get; set; }

    }

    public class RolesDetailModel {
        public string UserID { get; set; }
        public string[] Rolenames { get; set; }

    }

    #region LoginOutSide
    public class LoginWebModel
    {
        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "UID")]
        public string dbcode { get; set; }


    }

    public class LoginAppModel
    {
        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "UID")]
        public string dbcode { get; set; }


    }

    public class OTPLoginAppModel: LoginAppModel
    {
        [Required]
        [Display(Name = "OTP")]
        public string OTP { get; set; }

    }

    public class LoginTokenWebModel
    {
        public string Mobile { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
        public string dbcode { get; set; }



    }
    #endregion



}
