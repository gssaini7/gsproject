        public virtual DbSet<FeedbackModel> Feedbacks { get; set; }


		 modelBuilder.Entity<FeedbackModel>()
                  .ToTable("ta_Feedbacks", schemaName: UssSchemaName);


