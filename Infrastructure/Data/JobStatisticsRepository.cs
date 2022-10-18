using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class JobStatisticsRepository : IJobStatisticsRepository
    {
        private readonly JobContext _context;

        public JobStatisticsRepository(JobContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<JobStatistics>> GetJobsStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.JobStatistics
                .Where(s => s.ActualDate >= startDate && s.ActualDate <= endDate)
                .OrderBy(s => s.ActualDate)
                .AsQueryable()
                .ToListAsync();
        }
    }
}
