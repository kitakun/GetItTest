namespace Voronov.GetItTestApp.Core.Infrastructure
{
	public interface IEntityMapper
	{
		T Map<T>(object source);
	}
}
