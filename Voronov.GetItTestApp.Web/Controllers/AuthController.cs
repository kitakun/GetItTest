namespace Voronov.GetItTestApp.Web.Controllers
{
	using System;
	using System.Text;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Linq;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Http;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.Configuration;
	using Microsoft.AspNetCore.Cors;

	using Newtonsoft.Json;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.ViewModel.Auth;
	using Voronov.GetItTestApp.Web.Infrastructure;

	[Route("api/[controller]")]
	[EnableCors(Constants.AllCorsName)]
	public class AuthController : Controller
	{
		private readonly IRepository<User> _userRepo;
		private readonly IConfiguration _config;

		public const int TokenLifeTimeMinutes = 60;

		public AuthController(IRepository<User> userRepo, IConfiguration config)
		{
			_userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
			_config = config ?? throw new ArgumentNullException(nameof(config));
		}

		[HttpPost("token"), AllowAnonymous]
		public async Task Token([FromBody]AuthRequestModel request)
		{
			ClaimsIdentity identity = await GetIdentity(request.Login, request.Password);
			if (identity == null)
			{
				Response.StatusCode = 400;
				await Response.WriteAsync("{}");
				return;
			}

			var currentTime = DateTime.UtcNow;
			var secret = _config["Jwt:Key"];
			var issuer = _config["Jwt:Issuer"];

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
					issuer: issuer,
					audience: issuer,
					notBefore: currentTime,
					claims: identity.Claims,
					expires: currentTime.Add(TimeSpan.FromMinutes(TokenLifeTimeMinutes)),
					signingCredentials: signingCredentials);

			var encodedJwtHandler = new JwtSecurityTokenHandler();
			var encodedJwt = encodedJwtHandler.WriteToken(jwt);

			var response = new
			{
				username = identity.Claims.Single(s => s.Type == JwtRegisteredClaimNames.Sub).Value,
				id = identity.Claims.Single(s => s.Type == JwtRegisteredClaimNames.NameId).Value,
				access_token = encodedJwt
			};

			Response.ContentType = "application/json";
			await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
		}

		private async Task<ClaimsIdentity> GetIdentity(string username, string password)
		{
			ClaimsIdentity claimsIdentity = null;

			User person = await _userRepo.FirstOrDefault(x => x.Login == username && x.Password == password);

			if (person != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
					new Claim(JwtRegisteredClaimNames.Sub, $"{person.FirstName} {person.LastName}"),
					new Claim(JwtRegisteredClaimNames.NameId, person.Id.ToString())
				};
				claimsIdentity = new ClaimsIdentity(
					claims,
					"Token",
					ClaimsIdentity.DefaultNameClaimType,
					"no_roles");
			}

			return claimsIdentity;
		}
	}
}
