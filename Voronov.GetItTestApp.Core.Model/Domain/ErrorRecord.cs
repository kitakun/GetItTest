namespace Voronov.GetItTestApp.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ErrorRecord : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? Id { get; set; }

		public DateTime InputDate { get; set; }

		public string ShortDescription { get; set; }

		public string FullDescription { get; set; }

		public ErrorStatus Status { get; set; }

		public ErrorUrgency Urgency { get; set; }

		public ErrorImportanceType ImportanceType { get; set; }


		public virtual List<ErrorChangeRecord> Changes { get; set; }
	}
}
