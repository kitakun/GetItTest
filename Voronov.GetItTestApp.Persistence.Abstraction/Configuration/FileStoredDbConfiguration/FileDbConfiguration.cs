namespace Voronov.GetItTestApp.Persistence.Abstraction.Configuration.FileStoredDbConfiguration
{
	using System.IO;

	using Newtonsoft.Json;

	public class FileDbConfiguration : IDbConfiguration
	{
		private const string AppConfigName = "DbConfig.json";
		private static DbConnectionLink _connectionLink;

		public DbConnectionLink GetConfiguration()
		{
			if (_connectionLink == null)
			{
				foreach (string path in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.json",
					SearchOption.AllDirectories))
				{
					if (path.Contains(AppConfigName))
					{
						using (StreamReader reader = File.OpenText(path))
						{
							string rawConfig = reader.ReadToEnd();
							_connectionLink = JsonConvert.DeserializeObject<DbConnectionLink>(rawConfig);
							return _connectionLink;
						}
					}
				}
				_connectionLink = DbConnectionLink.Empty;
			}
			return _connectionLink;
		}
	}
}
