namespace Voronov.GetItTestApp.Mapper.Configuration
{
	using Autofac;
	using AutoMapper.Configuration;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.ViewModel.User;

	class UserConfiguration : IEntityMapperConfig
	{
		public void Config(MapperConfigurationExpression configuration, ILifetimeScope scope)
		{
			configuration.CreateMap<User, UpdateUserModel>()
				.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
				.ForMember(dest => dest.FirstName, m => m.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, m => m.MapFrom(src => src.LastName))
				.ReverseMap();

			configuration.CreateMap<User, UserViewModel>()
				.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
				.ForMember(dest => dest.Login, m => m.MapFrom(src => src.Login))
				.ForMember(dest => dest.FirstName, m => m.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, m => m.MapFrom(src => src.LastName))
				.ReverseMap();
		}
	}
}
