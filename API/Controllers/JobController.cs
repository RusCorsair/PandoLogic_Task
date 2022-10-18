using Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
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
            var statistics = await _repo.GetJobsStatisticsByDateRangeAsync(startDate, endDate);
            return Ok(_mapper
                .Map<IReadOnlyList<JobStatistics>, IReadOnlyList<JobStatisticsDto>>(statistics));
        }
    }
}
