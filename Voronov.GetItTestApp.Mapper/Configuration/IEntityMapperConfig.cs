namespace Voronov.GetItTestApp.Mapper.Configuration
{
	using Autofac;
	using AutoMapper.Configuration;

	public interface IEntityMapperConfig
	{
		void Config(MapperConfigurationExpression configuration, ILifetimeScope scope);
	}
}
