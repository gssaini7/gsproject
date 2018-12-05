        public virtual DbSet<MenuModel> Menus { get; set; }


		 modelBuilder.Entity<MenuModel>()
                  .ToTable("ta_Menus", schemaName: UssSchemaName);


