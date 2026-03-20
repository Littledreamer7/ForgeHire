using ForgeHire.Data;
using ForgeHire.Models.Company_Model;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Company_Info> GetByMobile(string mobile)
        {
            return await _db.Companies
                .FirstOrDefaultAsync(x => x.MobileNumber == mobile);
        }

        public async Task AddAsync(Company_Info company)
        {
            await _db.Companies.AddAsync(company);
        }
    }
}
