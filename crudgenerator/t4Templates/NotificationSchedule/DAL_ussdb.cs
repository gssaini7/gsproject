        public virtual DbSet<NotificationScheduleModel> NotificationSchedules { get; set; }


		 modelBuilder.Entity<NotificationScheduleModel>()
                  .ToTable("ta_NotificationSchedules", schemaName: UssSchemaName);


