namespace Voronov.GetItTestApp.Persistnce.EntityFramework
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction;

	class EfRepository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly IDbContext _dbContext;

		public EfRepository(IDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<T> ById(int id)
		{
			IQueryable<T> dbSet = _dbContext.GetDbSet<T>();
			T foundedElement = await dbSet
				.Where(x => x.Id == id)
				.ToAsyncEnumerable()
				.FirstOrDefault();

			return foundedElement;
		}

		public async Task<T> FirstOrDefault(Func<T, bool> expression)
		{
			IQueryable<T> dbSet = _dbContext.GetDbSet<T>();
			T foundedElement = await dbSet
				.Where(x => expression(x))
				.ToAsyncEnumerable()
				.FirstOrDefault();

			return foundedElement;
		}

		public async Task<IEnumerable<T>> All()
		{
			IQueryable<T> dbSet = _dbContext.GetDbSet<T>();
			List<T> foundedElements = await dbSet
				.ToListAsync();

			return foundedElements;
		}

		public async Task<IEnumerable<T>> Where<P>(Func<T, bool> expression, Expression<Func<T, P>> navigationPropertyPath = null)
		{
			IQueryable<T> dbSet = _dbContext.GetDbSet<T>();
			IQueryable<T> query = dbSet.Where(x => expression(x));
			if (navigationPropertyPath != null)
			{
				query = query.Include(navigationPropertyPath);
			}
			IEnumerable<T> foundedElements = await query
				.ToAsyncEnumerable()
				.ToArray();

			return foundedElements;
		}
	}
}
