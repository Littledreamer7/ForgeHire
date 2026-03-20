using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ForgeHire.Models.Company_Model;

namespace ForgeHire.Repositories.IRepositories
{
    public interface ICompanyRepository
    {
        Task<Company_Info> GetByMobile(string mobile);
        Task AddAsync(Company_Info company);
    }
}
