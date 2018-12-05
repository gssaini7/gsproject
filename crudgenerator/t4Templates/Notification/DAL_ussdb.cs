        public virtual DbSet<NotificationModel> Notifications { get; set; }


		 modelBuilder.Entity<NotificationModel>()
                  .ToTable("ta_Notifications", schemaName: UssSchemaName);


