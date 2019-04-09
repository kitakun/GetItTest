namespace Voronov.GetItTestApp.Workflow
{
	using System.Collections.Generic;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Workflow.Abstraction;

	class WorkflowService : IWorkflowService
	{
		public IEnumerable<ErrorStatus> GetNextAvailableStates(ErrorStatus currentStatus)
		{
			switch (currentStatus)
			{
				case ErrorStatus.New:
					return new ErrorStatus[] { ErrorStatus.Opened };

				case ErrorStatus.Opened:
					return new ErrorStatus[] { ErrorStatus.Resolved };

				case ErrorStatus.Resolved:
					return new ErrorStatus[] { ErrorStatus.Opened, ErrorStatus.Completed };

				case ErrorStatus.Completed:
					return new ErrorStatus[0];

				default:
					throw new System.NotImplementedException($"Type {currentStatus} not implemented");
			}
		}
	}
}
