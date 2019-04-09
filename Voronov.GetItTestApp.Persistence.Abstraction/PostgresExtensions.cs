namespace Voronov.GetItTestApp.Persistence.Abstraction
{
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration;

	public static class PostgresExtensions
	{
		public static string BuildConfigurationString(this DbConnectionLink config)
		{
			return $"Host={config.ServerUrl};Port={config.ServerPort};Database={config.DatabaseName};Username={config.UserName};Password={config.Password}";
		}
	}
}
