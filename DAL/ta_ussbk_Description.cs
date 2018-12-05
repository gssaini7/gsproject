namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ta_ussbk_Description
    {
        [Key]
        public int descriptionid { get; set; }

        public int? desctitile { get; set; }

        public int? desccategory { get; set; }

        [StringLength(50)]
        public string descvideolink { get; set; }

        [StringLength(50)]
        public string websitemodule { get; set; }

        [StringLength(500)]
        public string descanydescription { get; set; }

        public virtual ta_ussbk_categoryMaster ta_ussbk_categoryMaster { get; set; }

        public virtual ta_ussbk_TitleMaster ta_ussbk_TitleMaster { get; set; }
    }
}
