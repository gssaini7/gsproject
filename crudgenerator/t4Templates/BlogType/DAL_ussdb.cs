        public virtual DbSet<BlogTypeModel> BlogTypes { get; set; }


		 modelBuilder.Entity<BlogTypeModel>()
                  .ToTable("ta_BlogTypes", schemaName: UssSchemaName);


