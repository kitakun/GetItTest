namespace Voronov.GetItTestApp.Persistence.EntityFramework.DataServices
{
	using System;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;

	class ErrorRecordDataService : IErrorRecordDataService
	{
		private readonly IEntityFrameworkContext _dbContext;
		private readonly DbContext _rawContext;

		public ErrorRecordDataService(
			IEntityFrameworkContext dbContext,
			DbContext rawContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_rawContext = rawContext ?? throw new ArgumentNullException(nameof(rawContext));
		}

		public async Task<ErrorRecord> Insert(ErrorRecord entity)
		{
			entity.Id = null;
			entity.InputDate = DateTime.Now;

			var trackedLink  = await _dbContext.ErrorRecords.AddAsync(entity);
			await _rawContext.SaveChangesAsync();

			return trackedLink.Entity;
		}
	}
}
