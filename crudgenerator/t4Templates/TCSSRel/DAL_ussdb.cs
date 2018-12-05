        public virtual DbSet<TCSSRelModel> TCSSRels { get; set; }


		 modelBuilder.Entity<TCSSRelModel>()
                  .ToTable("ta_TCSSRels", schemaName: UssSchemaName);


