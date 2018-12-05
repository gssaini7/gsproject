namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ta_ussbk_ProjectDetail
    {
        [Key]
        public int amtdtid { get; set; }

        [Column(TypeName = "text")]
        public string projectdetail { get; set; }
    }
}
