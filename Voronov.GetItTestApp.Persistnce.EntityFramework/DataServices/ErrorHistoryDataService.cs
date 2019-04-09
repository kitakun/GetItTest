namespace Voronov.GetItTestApp.Persistence.EntityFramework.DataServices
{
	using System;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;

	class ErrorHistoryDataService : IErrorHistoryDataService
	{
		private readonly IEntityFrameworkContext _dbContext;
		private readonly DbContext _rawContext;

		public ErrorHistoryDataService(
			IEntityFrameworkContext dbContext,
			DbContext rawContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_rawContext = rawContext ?? throw new ArgumentNullException(nameof(rawContext));
		}

		/// <summary>
		/// Change history status with comment
		/// </summary>
		public async Task<ErrorChangeRecord> Insert(ErrorChangeRecord entity)
		{
			entity.Date = DateTime.Now;

			var trackedLinkTask = _dbContext.ErrorChangeRecords.AddAsync(entity);

			var errorRecordTask = _dbContext.ErrorRecords.FirstAsync(s => s.Id == entity.ErrorRecordId);

			await Task.WhenAll(trackedLinkTask, errorRecordTask).ConfigureAwait(false);

			var errorRecord = errorRecordTask.Result;
			errorRecord.Status = entity.Action;

			await _rawContext.SaveChangesAsync();

			return trackedLinkTask.Result.Entity;
		}
	}
}
