using ForgeHire.Models;

namespace ForgeHire.Repositories.IRepositories
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByMobile(string mobile);
        Task AddAsync(Candidate candidate);
    }
}
