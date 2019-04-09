namespace Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	using Voronov.GetItTestApp.Core.Model;

	public class EditableErrorRecord
	{
		[JsonProperty("id")]
		public int? Id { get; set; }

		[JsonProperty("shortDescription")]
		public string ShortDescription { get; set; }

		[JsonProperty("fullDescription")]
		public string FullDescription { get; set; }

		[JsonProperty("status")]
		public ErrorStatus Status { get; set; }

		[JsonProperty("urgency")]
		public ErrorUrgency Urgency { get; set; }

		[JsonProperty("criticalType")]
		public ErrorImportanceType CriticalType { get; set; }

		[JsonProperty("availableNextStates")]
		public IEnumerable<ErrorStatus> AvailableNextStates { get; set; }
	}
}
