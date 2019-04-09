namespace Voronov.GetItTestApp.Persistence.Abstraction.Configuration
{
	public class DbConnectionLink
	{
		public string ServerUrl { get; set; }

		public int ServerPort { get; set; }

		public string DatabaseName { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public static DbConnectionLink Empty => new DbConnectionLink()
		{
			DatabaseName = "temp",
			ServerUrl = "localhost",
			UserName = "postgres",
			Password = "admin"
		};
	}
}
