namespace Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview
{
	using Newtonsoft.Json;

	using Voronov.GetItTestApp.Core.Model;

	public class ErrorRecordChangeStateRequest
	{
		[JsonProperty("id")]
		public int ParentId { get; set; }

		[JsonProperty("comment")]
		public string Comment { get; set; }

		[JsonProperty("action")]
		public ErrorStatus NextState { get; set; }
	}
}
