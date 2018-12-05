        public virtual DbSet<LayoutModel> Layouts { get; set; }


		 modelBuilder.Entity<LayoutModel>()
                  .ToTable("ta_Layouts", schemaName: UssSchemaName);


