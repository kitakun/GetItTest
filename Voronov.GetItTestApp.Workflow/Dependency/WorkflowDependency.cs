namespace Voronov.GetItTestApp.Mapper.Dependency
{
	using Autofac;

	using Microsoft.Extensions.DependencyInjection;
	using Voronov.GetItTestApp.Core.Infrastructure.Dependency;
	using Voronov.GetItTestApp.Workflow;
	using Voronov.GetItTestApp.Workflow.Abstraction;

	public class WorkflowDependency : IDependency
	{
		public int Order => 10;

		public void Register(ContainerBuilder container, IServiceCollection services)
		{
			container.RegisterType<WorkflowService>().As<IWorkflowService>().SingleInstance();
		}
	}
}
