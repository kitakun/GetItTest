namespace Voronov.GetItTestApp.Web.Infrastructure
{
	using System.Collections.Generic;
	using System.Linq;

	using Autofac;

	using Microsoft.Extensions.DependencyInjection;

	using Voronov.GetItTestApp.Core.Infrastructure.Dependency;
	using Voronov.GetItTestApp.Core.Utilities;

	public static class DependencyRegister
	{
		internal static void Register(ContainerBuilder container, IServiceCollection services)
		{
			IEnumerable<IDependency> dependencyes = AssemblyFinder.FindAllInterfaces<IDependency>();

			dependencyes = dependencyes.ToList().OrderBy(x => x.Order);

			foreach (IDependency dependencyRegister in dependencyes)
			{
				dependencyRegister.Register(container, services);
			}
		}
	}
}
