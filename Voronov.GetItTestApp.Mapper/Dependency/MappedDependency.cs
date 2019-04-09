namespace Voronov.GetItTestApp.Mapper.Dependency
{
	using System.Linq;
	using System.Reflection;

	using AutoMapper;
	using Autofac;

	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyModel;
	using Voronov.GetItTestApp.Core.Infrastructure.Dependency;
	using Voronov.GetItTestApp.Core.Infrastructure;

	public class MappedDependency : IDependency
	{
		public int Order => 10;

		public void Register(ContainerBuilder container, IServiceCollection services)
		{
			AddAutoMapper(services, DependencyContext.Default);

			services.AddAutoMapper();

			container.RegisterType<EntityMapper>().As<IEntityMapper>().SingleInstance();
		}

		public static void AddAutoMapper(IServiceCollection services, DependencyContext dependencyContext)
		{
			services.AddAutoMapper(dependencyContext.RuntimeLibraries
				.SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load)));
		}
	}
}
