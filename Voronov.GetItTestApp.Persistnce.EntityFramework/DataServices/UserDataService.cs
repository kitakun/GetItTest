namespace Voronov.GetItTestApp.Persistence.EntityFramework.DataServices
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;

	class UserDataService : IUserDataService
	{
		private readonly IEntityFrameworkContext _dbContext;
		private readonly DbContext _rawContext;

		public UserDataService(
			IEntityFrameworkContext dbContext,
			DbContext rawContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_rawContext = rawContext ?? throw new ArgumentNullException(nameof(rawContext));
		}

		public Task<User> Insert(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateUserInfo(User changedUser)
		{
			User userForChange = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == changedUser.Id);

			userForChange.FirstName = changedUser.FirstName;
			userForChange.LastName = changedUser.LastName;

			await _rawContext.SaveChangesAsync();
		}

		public async Task<int> UserIdByAccount(string account)
		{
			int? foundedUserId = await _dbContext.Users.Where(u => u.Login == account).Select(s => s.Id).FirstOrDefaultAsync();
			return foundedUserId ?? 0;
		}
	}
}
