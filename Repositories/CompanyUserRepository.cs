using ForgeHire.Data;
using ForgeHire.Models.User_Model;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
    public class CompanyUserRepository : ICompanyUserRepository
    {
        private readonly AppDbContext _db;

        public CompanyUserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(CompanyUser companyUser)
        {
            await _db.CompanyUsers.AddAsync(companyUser);
        }

        public async Task<CompanyUser?> GetByUserId(int userId)
        {
            return await _db.CompanyUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive);
        }
    }
}
