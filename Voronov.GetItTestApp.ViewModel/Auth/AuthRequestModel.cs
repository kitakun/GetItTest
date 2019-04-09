namespace Voronov.GetItTestApp.ViewModel.Auth
{
	using Newtonsoft.Json;

	public class AuthRequestModel
	{
		[JsonProperty("login")]
		public string Login { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
