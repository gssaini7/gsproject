        public virtual DbSet<MainDatabasesModel> MainDatabasess { get; set; }


		 modelBuilder.Entity<MainDatabasesModel>()
                  .ToTable("ta_MainDatabasess", schemaName: UssSchemaName);


