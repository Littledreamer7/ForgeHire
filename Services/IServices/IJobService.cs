using ForgeHire.Dtos.Job_Dtos;

namespace ForgeHire.Services
{
    public interface IJobService
    {
        Task<IEnumerable<JobResponseDto>> GetAllJobs();

        Task<JobResponseDto> GetJobById(int id);

        Task<JobResponseDto> CreateJob(CreateJobDto dto);

        Task<JobResponseDto> UpdateJob(int id, UpdateJobDto dto);

        Task<bool> DeleteJob(int id);
    }
}