        public virtual DbSet<SettingsModel> Settingss { get; set; }


		 modelBuilder.Entity<SettingsModel>()
                  .ToTable("ta_Settingss", schemaName: UssSchemaName);


