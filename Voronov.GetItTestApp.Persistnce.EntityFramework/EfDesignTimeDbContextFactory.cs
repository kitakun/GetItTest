namespace Voronov.GetItTestApp.Persistence.EntityFramework
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Design;

	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration;
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration.FileStoredDbConfiguration;
	using Voronov.GetItTestApp.Persistnce.EntityFramework;

	/// <summary>
	/// For dotnet ef migrations
	/// 
	/// dotnet ef migrations add StartupMigration --startup-project ../Voronov.GetItTestApp.Web
	/// dotnet ef database update --startup-project ../Voronov.GetItTestApp.Web --context EntityFrameworkContext
	/// dotnet ef migrations remove --startup-project ../Voronov.GetItTestApp.Web --context EntityFrameworkContext
	/// </summary>
	public class EfDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EntityFrameworkContext>
	{
		private readonly IDbConfiguration _dbConfiguration;

		public EfDesignTimeDbContextFactory()
		{
			_dbConfiguration = new FileDbConfiguration();
		}

		public EntityFrameworkContext CreateDbContext(string[] args)
		{
			var config = _dbConfiguration.GetConfiguration();
			var connectionString = config.BuildConfigurationString();

			var builder = new DbContextOptionsBuilder<EntityFrameworkContext>();
			builder.UseNpgsql(connectionString);

			return new EntityFrameworkContext(_dbConfiguration);
		}
	}
}
