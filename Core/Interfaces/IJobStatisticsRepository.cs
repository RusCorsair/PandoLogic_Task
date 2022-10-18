using Core.Entities;

namespace Core.Interfaces
{
    public interface IJobStatisticsRepository
    {
        Task<IReadOnlyList<JobStatistics>> GetJobsStatisticsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
