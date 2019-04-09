namespace Voronov.GetItTestApp.Persistence.Abstraction
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using Voronov.GetItTestApp.Core.Model;

	public interface IRepository<T> where T : class, IEntity
	{
		Task<T> ById(int id);

		Task<T> FirstOrDefault(Func<T, bool> expression);

		Task<IEnumerable<T>> Where<P>(Func<T, bool> expression, Expression<Func<T, P>> navigationPropertyPath = null);

		Task<IEnumerable<T>> All();
	}
}
