namespace Voronov.GetItTestApp.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;

	using Voronov.GetItTestApp.Core.Infrastructure;
	using Voronov.GetItTestApp.Core.Model;
	using Voronov.GetItTestApp.Persistence.Abstraction;
	using Voronov.GetItTestApp.Persistence.Abstraction.DataServices;
	using Voronov.GetItTestApp.ViewModel.ErrorRecordsPreview;
	using Voronov.GetItTestApp.Web.Infrastructure;
	using Voronov.GetItTestApp.Workflow.Abstraction;

	[Authorize]
	[Route("api/[controller]")]
	[EnableCors(Constants.AllCorsName)]
	public class ErrorRecordsController : Controller
	{
		private readonly IEntityMapper _mapper;
		private readonly IWorkflowService _workflowService;
		private readonly IUserDataService _userDataService;

		private readonly IRepository<ErrorRecord> _errorRecordRepo;
		private readonly IErrorRecordDataService _errorDataService;

		private readonly IRepository<ErrorChangeRecord> _errorChangeRepo;
		private readonly IErrorHistoryDataService _errorChangeDataService;


		public ErrorRecordsController(
			IEntityMapper mapper,
			IRepository<ErrorRecord> errorRecordRepo,
			IErrorRecordDataService errorDataService,
			IRepository<ErrorChangeRecord> changeRepo,
			IErrorHistoryDataService errorChangeDataService,
			IWorkflowService workflowService,
			IUserDataService userDataService)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_errorRecordRepo = errorRecordRepo ?? throw new ArgumentNullException(nameof(errorRecordRepo));
			_errorDataService = errorDataService ?? throw new ArgumentNullException(nameof(errorDataService));
			_errorChangeRepo = changeRepo ?? throw new ArgumentNullException(nameof(changeRepo));
			_errorChangeDataService = errorChangeDataService ?? throw new ArgumentNullException(nameof(errorChangeDataService));
			_workflowService = workflowService ?? throw new ArgumentNullException(nameof(workflowService));
			_userDataService = userDataService;
		}

		[HttpPost("[Action]")]
		public async Task<IEnumerable<ErrorRecordPreview>> PreviewList()
		{
			IEnumerable<ErrorRecord> rawElements = await _errorRecordRepo.All();
			IEnumerable<ErrorRecordPreview> mappedElements = _mapper.Map<IEnumerable<ErrorRecordPreview>>(rawElements);
			return mappedElements;
		}

		[HttpPost("[Action]")]
		public async Task<EditableErrorRecord> ById([FromQuery]int id)
		{
			ErrorRecord targetRecord = await _errorRecordRepo.ById(id);
			EditableErrorRecord model = _mapper.Map<EditableErrorRecord>(targetRecord);
			model.AvailableNextStates = _workflowService.GetNextAvailableStates(model.Status);
			return model;
		}

		[HttpPost("[Action]")]
		public async Task<int> Create([FromBody]EditableErrorRecord entity)
		{
			if (entity.Id.HasValue)
			{
				throw new ArgumentException($"Can't create record with Id");
			}

			ErrorRecord coreEntity = _mapper.Map<ErrorRecord>(entity);
			ErrorRecord insertedEntity = await _errorDataService.Insert(coreEntity);
			return insertedEntity.Id.Value;
		}

		[HttpPost("[Action]")]
		public async Task<bool> Change([FromBody]ErrorRecordChangeStateRequest model)
		{
			ErrorChangeRecord coreEntity = _mapper.Map<ErrorChangeRecord>(model);

			int currentUserId = await _userDataService.UserIdByAccount(User.Identity.Name);
			coreEntity.ChangedById = currentUserId;
			ErrorChangeRecord insertedEntity = await _errorChangeDataService.Insert(coreEntity);

			return insertedEntity.Id > 0;
		}

		[HttpPost("[Action]")]
		public async Task<IEnumerable<ErrorRecordHistory>> History([FromQuery]int id)
		{
			var allHistory = await _errorChangeRepo.Where(x => x.ErrorRecordId == id, s => s.ChangedBy);
			var mapped = _mapper.Map<IEnumerable<ErrorRecordHistory>>(allHistory);
			return mapped;
		}
	}
}
