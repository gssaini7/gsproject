namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ta_Manager
    {
        [Key]
        public long mid { get; set; }

        public string mguid { get; set; }

        public string mcontent { get; set; }

        [StringLength(50)]
        public string mrowtype { get; set; }
    }
}
