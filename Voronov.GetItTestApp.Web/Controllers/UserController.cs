namespace Voronov.GetItTestApp.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;

	using Voronov.GetItTestApp.Core.Infrastructure;
	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;
	using Voronov.GetItTestApp.ViewModel.User;
	using Voronov.GetItTestApp.Web.Infrastructure;

	[Authorize]
	[Route("api/[controller]")]
	[EnableCors(Constants.AllCorsName)]
	public class UserController : Controller
	{
		private readonly IEntityMapper _mapper;
		private readonly IRepository<User> _userRepository;
		private readonly IUserDataService _userDataService;

		public UserController(
			IEntityMapper mapper,
			IRepository<User> userRepository,
			IUserDataService userDataService)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			_userDataService = userDataService ?? throw new ArgumentNullException(nameof(userDataService));
		}

		[HttpPost("[Action]")]
		public async Task<User> LoadById([FromQuery]int id)
		{
			User loadedUser = await _userRepository.ById(id);
			return loadedUser;
		}

		[HttpPost("[Action]")]
		public async Task UpdateUserInfo([FromBody]UpdateUserModel user)
		{
			User changedModel = _mapper.Map<User>(user);
			await _userDataService.UpdateUserInfo(changedModel);
		}

		[HttpPost("[Action]")]
		public async Task<IEnumerable<UserViewModel>> List()
		{
			IEnumerable<User> allUsers = await _userRepository.All();
			IEnumerable<UserViewModel> mapped = _mapper.Map<IEnumerable<UserViewModel>>(allUsers);
			return mapped;
		}
	}
}
