namespace Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview
{
	using System;

	using Voronov.GetItTestApp.Core.Model;

	public class ErrorRecordPreview
	{
		public int Id { get; set; }

		public DateTime InputDate { get; set; }

		public string Description { get; set; }

		public ErrorStatus Status { get; set; }

		public ErrorUrgency Urgency { get; set; }

		public ErrorImportanceType ImportanceType { get; set; }
	}
}
