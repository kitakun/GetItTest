namespace Voronov.GetItTestApp.Persistence.EntityFramework
{
	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;

	public interface IEntityFrameworkContext
	{
		DbSet<ErrorRecord> ErrorRecords { get; }

		DbSet<ErrorChangeRecord> ErrorChangeRecords { get; }

		DbSet<User> Users { get; }
	}
}
