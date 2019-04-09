namespace Voronov.GetItTestApp.Persistnce.EntityFramework
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration;
	using Voronov.GetItTestApp.Persistence.EntityFramework;

	public class EntityFrameworkContext : DbContext, IEntityFrameworkContext, IDbContext
	{
		private readonly IDbConfiguration _dbConfiguration;

		public DbSet<ErrorRecord> ErrorRecords { get; internal set; }

		public DbSet<ErrorChangeRecord> ErrorChangeRecords { get; internal set; }

		public DbSet<User> Users { get; internal set; }

		public EntityFrameworkContext(IDbConfiguration dbConfiguration)
		{
			_dbConfiguration = dbConfiguration ?? throw new ArgumentNullException(nameof(dbConfiguration));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//Move to interface/mapper
			builder.ForNpgsqlUseIdentityColumns();
			builder.Entity<User>().HasKey(m => m.Id);

			builder.Entity<ErrorRecord>().HasKey(m => m.Id);
			builder.Entity<ErrorRecord>().Property<DateTime>("InputDate");
			builder.Entity<ErrorRecord>().Property(s => s.Id).ValueGeneratedOnAdd().UseNpgsqlSerialColumn();
			builder.Entity<ErrorRecord>().HasMany(s => s.Changes).WithOne(s => s.ErrorRecord).HasForeignKey(f => f.ErrorRecordId);

			builder.Entity<ErrorChangeRecord>().Property(s => s.Id).ValueGeneratedOnAdd().UseNpgsqlSerialColumn();
			builder.Entity<ErrorChangeRecord>().HasKey(m => m.Id);
			builder.Entity<ErrorChangeRecord>().Property<DateTime>("Date");
			builder.Entity<ErrorChangeRecord>().HasOne(s => s.ChangedBy);
			builder.Entity<User>().Property(s => s.Id).ValueGeneratedOnAdd().UseNpgsqlSerialColumn();

#if DEBUG
			SeedDevData(builder);
#endif
			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			DbConnectionLink config = _dbConfiguration.GetConfiguration();
			string configString = config.BuildConfigurationString();
			optionsBuilder.UseNpgsql(configString);
		}

		public IQueryable<T> GetDbSet<T>() where T : class, IEntity
		{
			IQueryable<T> typedSet = this.Set<T>();
			return typedSet;
		}

		private void SeedDevData(ModelBuilder builder)
		{
			User[] users = new[]{
				new User {
					Id = -1,
					Login="admin",
					Password="adm",
					FirstName="SuperUser",
					LastName="Wabwabwba"
				},
				new User {
					Id = -2,
					Login="ksh",
					Password="ksh",
					FirstName="Kesha",
					LastName="Karpik"
				},
				new User {
					Id = -3,
					Login="zg",
					Password="zg",
					FirstName="Zegerman",
					LastName="Underfatch"
				}
			};
			builder.Entity<User>().HasData(users.ToArray());

			var prepChanges = new List<ErrorChangeRecord>();
			for (int i = 1; i < 10; i++)
			{
				prepChanges.Add(new ErrorChangeRecord
				{
					Id = -i,
					Comment = "Temp Comment",
					Date = DateTime.Now.AddDays(-i),
					ChangedById = -1,
					ErrorRecordId = -i,
					ChangedBy = null,
					ErrorRecord = null
				});
			}
			builder.Entity<ErrorChangeRecord>().HasData(prepChanges.ToArray());

			var prepList = new List<ErrorRecord>();
			for (int i = 1; i < 10; i++)
			{
				prepList.Add(new ErrorRecord
				{
					Id = -i,
					FullDescription = $"side bar {i} side bar side bar side bar {i} side bar side bar side bar{i}  side bar ",
					ShortDescription = $"side bar {i} ...",
					ImportanceType = ErrorImportanceType.Accident,
					InputDate = DateTime.Now.AddMinutes(i),
					Status = ErrorStatus.New,
					Urgency = ErrorUrgency.NonEmergent,
					Changes = new List<ErrorChangeRecord>()
				});
			}
			builder.Entity<ErrorRecord>().HasData(prepList.ToArray());
		}
	}
}
