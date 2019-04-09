namespace Voronov.GetItTestApp.Workflow.Abstraction
{
	using System.Collections.Generic;

	using Voronov.GetItTestApp.Core.Model;

	public interface IWorkflowService
	{
		IEnumerable<ErrorStatus> GetNextAvailableStates(ErrorStatus currentStatus);
	}
}
