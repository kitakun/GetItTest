namespace Voronov.GetItTestApp.Mapper
{
	using System;
	using System.Collections.Generic;

	using Autofac;
	using AutoMapper;
	
	using Voronov.GetItTestApp.Core.Infrastructure;
	using Voronov.GetItTestApp.Core.Utilities;
	using Voronov.GetItTestApp.Mapper.Configuration;

	using MapperConfiguration = AutoMapper.Configuration;

	class EntityMapper : IEntityMapper
	{
		private AutoMapper.IMapper _mapper;
		private readonly ILifetimeScope _scope;

		public EntityMapper(ILifetimeScope scope)
		{
			_scope = scope ?? throw new ArgumentNullException(nameof(scope));

			Register(scope);
		}

		private void Register(ILifetimeScope serviceProvier)
		{
			var mapperConfiguration = new MapperConfiguration.MapperConfigurationExpression();

			IEnumerable<IEntityMapperConfig> configurators = AssemblyFinder.FindAllInterfaces<IEntityMapperConfig>();
			foreach (IEntityMapperConfig mapperConfigurationInstance in configurators)
			{
				mapperConfigurationInstance.Config(mapperConfiguration, serviceProvier);
			}

			Mapper.Initialize(mapperConfiguration);
			IMapper createdMapper = Mapper.Configuration.CreateMapper();
			_mapper = createdMapper;
		}

		public T Map<T>(object source)
		{
			return _mapper.Map<T>(source);
		}
	}
}
