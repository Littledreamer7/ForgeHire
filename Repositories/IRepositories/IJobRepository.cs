using ForgeHire.Models.Job_Model;

namespace ForgeHire.Repositories.IRepositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllAsync();

        Task<Job?> GetJobByIdAsync(int id);

        Task<IEnumerable<Job>> GetJobsByCompanyIdAsync(int companyId);

        Task<Job> CreateJobAsync(Job job);

        Task UpdateJobAsync(Job job);

        Task<bool> DeleteJobAsync(int id);
    }
}