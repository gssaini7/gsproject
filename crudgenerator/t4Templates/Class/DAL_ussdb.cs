        public virtual DbSet<ClassModel> Classs { get; set; }


		 modelBuilder.Entity<ClassModel>()
                  .ToTable("ta_Classs", schemaName: UssSchemaName);


