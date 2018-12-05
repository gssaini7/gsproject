        public virtual DbSet<ImageGalleryModel> ImageGallerys { get; set; }


		 modelBuilder.Entity<ImageGalleryModel>()
                  .ToTable("ta_ImageGallerys", schemaName: UssSchemaName);


