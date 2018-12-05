namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ussdbEntities")
        {
        }

        public virtual DbSet<Ta_Manager> Ta_Manager { get; set; }
        public virtual DbSet<ta_ussbk_categoryMaster> ta_ussbk_categoryMaster { get; set; }
        public virtual DbSet<ta_ussbk_clientrecord> ta_ussbk_clientrecord { get; set; }
        public virtual DbSet<TA_ussbk_Coupons> TA_ussbk_Coupons { get; set; }
        public virtual DbSet<ta_ussbk_Description> ta_ussbk_Description { get; set; }
        public virtual DbSet<ta_ussbk_ProjectDetail> ta_ussbk_ProjectDetail { get; set; }
        public virtual DbSet<ta_ussbk_TitleMaster> ta_ussbk_TitleMaster { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ta_ussbk_categoryMaster>()
                .HasMany(e => e.ta_ussbk_Description)
                .WithOptional(e => e.ta_ussbk_categoryMaster)
                .HasForeignKey(e => e.desccategory);

            modelBuilder.Entity<ta_ussbk_clientrecord>()
                .Property(e => e.clientemail)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_clientrecord>()
                .Property(e => e.contactmobile)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_clientrecord>()
                .Property(e => e.clientaddress)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_clientrecord>()
                .Property(e => e.formodule)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_clientrecord>()
                .Property(e => e.userrole)
                .IsUnicode(false);

            modelBuilder.Entity<TA_ussbk_Coupons>()
                .Property(e => e.couponcode)
                .IsUnicode(false);

            modelBuilder.Entity<TA_ussbk_Coupons>()
                .Property(e => e.cmsgforuser)
                .IsUnicode(false);

            modelBuilder.Entity<TA_ussbk_Coupons>()
                .Property(e => e.cAdminRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_Description>()
                .Property(e => e.descvideolink)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_Description>()
                .Property(e => e.websitemodule)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_ProjectDetail>()
                .Property(e => e.projectdetail)
                .IsUnicode(false);

            modelBuilder.Entity<ta_ussbk_TitleMaster>()
                .HasMany(e => e.ta_ussbk_Description)
                .WithOptional(e => e.ta_ussbk_TitleMaster)
                .HasForeignKey(e => e.desctitile);
        }
    }
}
