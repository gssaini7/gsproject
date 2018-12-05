namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TA_ussbk_Coupons
    {
        [Key]
        public int couponid { get; set; }

        [StringLength(15)]
        public string couponcode { get; set; }

        [StringLength(100)]
        public string cmsgforuser { get; set; }

        [StringLength(100)]
        public string cAdminRemarks { get; set; }

        public bool? cblocked { get; set; }

        public int? camount { get; set; }
    }
}
