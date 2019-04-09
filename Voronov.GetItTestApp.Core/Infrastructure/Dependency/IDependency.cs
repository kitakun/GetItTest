namespace Voronov.GetItTestApp.Core.Infrastructure.Dependency
{
	using Autofac;

	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	/// Interface for generic registartion dependencies from different projects
	/// </summary>
	public interface IDependency
	{
		/// <summary>
		/// Sort order of dependency registration
		/// </summary>
		int Order { get; }

		/// <summary>
		/// Registration method
		/// builder.RegisterType<MyType>().As<IMyType>();
		/// </summary>
		void Register(ContainerBuilder container, IServiceCollection services);
	}
}
