using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace R.BusinessEntities
{
    public class EntityCommonModel 
    {
        public DateTime? createdate { get; set; }
        public DateTime? LastUpdatedate { get; set; }
        public bool? isPublished { get; set; }
        public Guid? createdBy { get; set; }
        public string createdByName { get; set; }
        public string remarks { get; set; }
        public bool? istrash { get; set; }

    }

   

}
