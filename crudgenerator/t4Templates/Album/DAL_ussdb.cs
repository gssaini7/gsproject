        public virtual DbSet<AlbumModel> Albums { get; set; }


		 modelBuilder.Entity<AlbumModel>()
                  .ToTable("ta_Albums", schemaName: UssSchemaName);


