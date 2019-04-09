namespace Voronov.GetItTestApp.Persistence.Abstraction.Configuration
{
	public interface IDbConfiguration
	{
		DbConnectionLink GetConfiguration();
	}
}
