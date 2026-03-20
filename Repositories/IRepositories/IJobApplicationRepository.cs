using ForgeHire.Models.Job_Application_Model;

namespace ForgeHire.Repositories.IRepositories
{
    public interface IJobApplicationRepository
    {
        Task<bool> ExistsAsync(int jobId, int candidateId, CancellationToken ct = default);

        Task AddAsync(JobApplication application, CancellationToken ct = default);

        Task<List<JobApplication>> GetByCandidateAsync(int candidateId, CancellationToken ct = default);

        Task<List<JobApplication>> GetByJobAsync(int jobId, CancellationToken ct = default);

        Task<JobApplication?> GetByIdAsync(int id, CancellationToken ct = default);

        Task SaveAsync(CancellationToken ct = default);
    }
}