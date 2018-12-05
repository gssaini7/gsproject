using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace crudgenerator.Models
{
    public class CRUDMainModel
    {
        public string MainModelName { get; set; }
    }
    public class CRUDElementsModel
    {
        public string ElementModelName { get; set; }
        public string ElementType { get; set; }
    }

    


}