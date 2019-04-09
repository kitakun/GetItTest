namespace Voronov.GetItTestApp.ViewModel.User
{
	using Newtonsoft.Json;

	public class UpdateUserModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }
	}
}
