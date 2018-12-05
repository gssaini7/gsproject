        public virtual DbSet<SiteModel> Sites { get; set; }


		 modelBuilder.Entity<SiteModel>()
                  .ToTable("ta_Sites", schemaName: UssSchemaName);


