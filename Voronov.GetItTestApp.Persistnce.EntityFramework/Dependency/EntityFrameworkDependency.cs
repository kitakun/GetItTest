namespace Voronov.GetItTestApp.Persistnce.EntityFramework
{
	using Autofac;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	using Voronov.GetItTestApp.Core.Infrastructure.Dependency;
	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration;
	using Voronov.GetItTestApp.Persistence.Abstraction.Configuration.FileStoredDbConfiguration;
	using Voronov.GetItTestApp.Persistence.EntityFramework;

	public class EntityFrameworkDependency : IDependency
	{
		public int Order => 50;

		public void Register(ContainerBuilder container, IServiceCollection services)
		{
			services.AddEntityFrameworkNpgsql()
				.AddDbContext<EntityFrameworkContext>()
				.BuildServiceProvider();

			container.RegisterType<FileDbConfiguration>().As<IDbConfiguration>().SingleInstance();

			container.RegisterType<EntityFrameworkContext>()
				.As<DbContext>()
				.As<IEntityFrameworkContext>()
				.As<IDbContext>()
				.InstancePerLifetimeScope();

			container.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));
		}
	}
}
