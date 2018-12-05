namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ta_ussbk_clientrecord
    {
        [Key]
        public int recordid { get; set; }

        [StringLength(100)]
        public string productname { get; set; }

        [StringLength(50)]
        public string productid { get; set; }

        [StringLength(100)]
        public string businessname { get; set; }

        [StringLength(100)]
        public string clientname { get; set; }

        [StringLength(50)]
        public string clientemail { get; set; }

        [StringLength(12)]
        public string contactmobile { get; set; }

        [StringLength(200)]
        public string clientaddress { get; set; }

        [StringLength(100)]
        public string formodule { get; set; }

        [StringLength(100)]
        public string userrole { get; set; }

        public bool? userblocked { get; set; }

        public DateTime? expirydate { get; set; }

        [StringLength(250)]
        public string upassword { get; set; }

        public string remarks { get; set; }
    }
}
