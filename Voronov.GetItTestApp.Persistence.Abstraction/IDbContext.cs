namespace Voronov.GetItTestApp.Persistence.Abstraction
{
	using System.Linq;

	using Voronov.GetItTestApp.Core.Model;

	public interface IDbContext
	{
		IQueryable<T> GetDbSet<T>() where T : class, IEntity;
	}
}
