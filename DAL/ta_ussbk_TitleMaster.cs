namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ta_ussbk_TitleMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ta_ussbk_TitleMaster()
        {
            ta_ussbk_Description = new HashSet<ta_ussbk_Description>();
        }

        [Key]
        public int titleid { get; set; }

        [StringLength(200)]
        public string titlename { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ta_ussbk_Description> ta_ussbk_Description { get; set; }
    }
}
