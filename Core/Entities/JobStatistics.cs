namespace Core.Entities
{
    public class JobStatistics : BaseEntity
    {
        public int JobViews { get; set; }
        public int PredictedJobViews { get; set; }
        public int ActiveJobs { get; set; }
        public DateTime ActualDate { get; set; }
    }
}
