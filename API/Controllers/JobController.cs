using Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using AutoMapper;
using NLog;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IJobStatisticsRepository _repo;
        private readonly IMapper _mapper;
        public JobController(IJobStatisticsRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<JobStatisticsDto>>> GetJobStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            _logger.Info($"Received request for statistics between [{startDate.ToShortDateString}] and [{endDate.ToShortDateString}].");

            var statistics = await _repo.GetJobsStatisticsByDateRangeAsync(startDate, endDate);
            return Ok(_mapper
                .Map<IReadOnlyList<JobStatistics>, IReadOnlyList<JobStatisticsDto>>(statistics));
        }
    }
}
