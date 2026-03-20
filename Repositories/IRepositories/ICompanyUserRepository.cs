using ForgeHire.Models.User_Model;

namespace ForgeHire.Repositories.IRepositories
{
    public interface ICompanyUserRepository
    {
        Task AddAsync(CompanyUser companyUser);
        Task<CompanyUser?> GetByUserId(int userId);
    }
}
