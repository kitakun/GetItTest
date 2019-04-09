namespace Voronov.GetItTestApp.Persistence.Abstraction.DataServices
{
	using System.Threading.Tasks;

	using Voronov.GetItTestApp.Core.Model;

	public interface IDataService<T> where T : class, IEntity
	{
		Task<T> Insert(T entity);
	}
}
