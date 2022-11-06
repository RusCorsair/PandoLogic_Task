using Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using AutoMapper;
using NLog;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobStatisticsRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public JobController(IJobStatisticsRepository repo, IMapper mapper, ILoggerService loggerService)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<JobStatisticsDto>>> GetJobStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            _loggerService.LogInfo($"Received request for statistics between [{startDate.ToShortDateString}] and [{endDate.ToShortDateString}].");

            var statistics = await _repo.GetJobsStatisticsByDateRangeAsync(startDate, endDate);

            _loggerService.LogInfo($"Returning response for statistics between [{startDate.ToShortDateString}] and [{endDate.ToShortDateString}], [{statistics.Count}] results in total.");

            return Ok(_mapper
                .Map<IReadOnlyList<JobStatistics>, IReadOnlyList<JobStatisticsDto>>(statistics));
        }
    }
}
