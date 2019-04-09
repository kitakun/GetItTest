namespace Voronov.GetItTestApp.Mapper.Configuration
{
	using Autofac;
	using AutoMapper.Configuration;

	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview;

	class ErrorRecordsConfiguration : IEntityMapperConfig
	{
		public void Config(MapperConfigurationExpression configuration, ILifetimeScope scope)
		{
			configuration.CreateMap<ErrorRecord, ErrorRecordPreview>()
				.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
				.ForMember(dest => dest.Description, m => m.MapFrom(src => src.ShortDescription))
				.ForMember(dest => dest.ImportanceType, m => m.MapFrom(src => src.ImportanceType))
				.ForMember(dest => dest.InputDate, m => m.MapFrom(src => src.InputDate))
				.ForMember(dest => dest.Status, m => m.MapFrom(src => src.Status))
				.ForMember(dest => dest.Urgency, m => m.MapFrom(src => src.Urgency))
				.ReverseMap();

			configuration.CreateMap<ErrorRecord, EditableErrorRecord>()
				.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
				.ForMember(dest => dest.ShortDescription, m => m.MapFrom(src => src.ShortDescription))
				.ForMember(dest => dest.FullDescription, m => m.MapFrom(src => src.FullDescription))
				.ForMember(dest => dest.CriticalType, m => m.MapFrom(src => src.ImportanceType))
				.ForMember(dest => dest.Status, m => m.MapFrom(src => src.Status))
				.ForMember(dest => dest.Urgency, m => m.MapFrom(src => src.Urgency))
				.ReverseMap();

			configuration.CreateMap<ErrorChangeRecord, ErrorRecordHistory>()
				.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
				.ForMember(dest => dest.Action, m => m.MapFrom(src => src.Action))
				.ForMember(dest => dest.Comment, m => m.MapFrom(src => src.Comment))
				.ForMember(dest => dest.Date, m => m.MapFrom(src => src.Date))
				.ForMember(dest => dest.ChangedBy, m => m.MapFrom(src => $"{src.ChangedBy.FirstName} {src.ChangedBy.LastName}"))
				.ReverseMap();

			configuration.CreateMap<ErrorChangeRecord, ErrorRecordChangeStateRequest>()
				.ForMember(dest => dest.ParentId, m => m.MapFrom(src => src.ErrorRecordId))
				.ForMember(dest => dest.NextState, m => m.MapFrom(src => src.Action))
				.ForMember(dest => dest.Comment, m => m.MapFrom(src => src.Comment))
				.ReverseMap();
		}
	}
}
