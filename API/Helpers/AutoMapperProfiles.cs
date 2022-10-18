using AutoMapper;
using API.DTOs;
using Core.Entities;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobStatistics, JobStatisticsDto>();
        }
    }
}
 