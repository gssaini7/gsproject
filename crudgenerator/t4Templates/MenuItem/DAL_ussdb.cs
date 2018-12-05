        public virtual DbSet<MenuItemModel> MenuItems { get; set; }


		 modelBuilder.Entity<MenuItemModel>()
                  .ToTable("ta_MenuItems", schemaName: UssSchemaName);


