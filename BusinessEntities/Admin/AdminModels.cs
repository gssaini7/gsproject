
using System.ComponentModel.DataAnnotations;


namespace R.BusinessEntities
{
    // Models used as parameters to Admins by GS.

    public class RolesModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

}
