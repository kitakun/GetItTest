namespace Voronov.GetItTestApp.Core.Model
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ErrorChangeRecord : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? Id { get; set; }

		public DateTime Date { get; set; }

		public string Comment { get; set; }

		public ErrorStatus Action { get; set; }


		[ForeignKey("ChangedById")]
		public virtual User ChangedBy { get; set; }
		public int ChangedById { get; set; }

		[ForeignKey("ErrorRecordId")]
		public virtual ErrorRecord ErrorRecord { get; set; }
		public int ErrorRecordId { get; set; }
	}
}
