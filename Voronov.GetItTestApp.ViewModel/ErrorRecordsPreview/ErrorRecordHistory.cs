namespace Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview
{
	using System;

	using Voronov.GetItTestApp.Core.Model;

	public class ErrorRecordHistory
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string Comment { get; set; }

		public ErrorStatus Action { get; set; }

		public string ChangedBy { get; set; }
	}
}
