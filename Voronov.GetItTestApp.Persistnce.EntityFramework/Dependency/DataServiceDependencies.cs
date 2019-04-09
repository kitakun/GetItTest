namespace Voronov.GetItTestApp.Persistence.EntityFramework.Dependency
{
	using Autofac;

	using Microsoft.Extensions.DependencyInjection;

	using Voronov.GetItTestApp.Core.Infrastructure.Dependency;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;
	using Voronov.GetItTestApp.Persistence.EntityFramework.DataServices;

	public class DataServiceDependencies : IDependency
	{
		public int Order => 55;

		public void Register(ContainerBuilder container, IServiceCollection services)
		{
			container.RegisterType<UserDataService>().As<IUserDataService>().AsImplementedInterfaces();

			container.RegisterType<ErrorRecordDataService>().As<IErrorRecordDataService>().AsImplementedInterfaces();
			container.RegisterType<ErrorHistoryDataService>().As<IErrorHistoryDataService>().AsImplementedInterfaces();
		}
	}
}
