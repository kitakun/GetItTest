namespace Voronov.GetItTestApp.Persistence.Abstraction.DataServices
{
	using System.Threading.Tasks;

	using Voronov.GetItTestApp.Core.Model;

	public interface IUserDataService : IDataService<User>
	{
		Task UpdateUserInfo(User changedUser);

		Task<int> UserIdByAccount(string account);
	}
}
