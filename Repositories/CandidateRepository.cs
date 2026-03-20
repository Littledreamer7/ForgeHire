using ForgeHire.Data;
using ForgeHire.Models;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
        public class CandidateRepository : ICandidateRepository
        {
            private readonly AppDbContext _db;

            public CandidateRepository(AppDbContext db)
            {
                _db = db;
            }

            public async Task<Candidate?> GetByMobile(string mobile)
            {
                return await _db.Candidates
                    .FirstOrDefaultAsync(x => x.MobileNumber == mobile);
            }

            public async Task AddAsync(Candidate candidate)
            {
                await _db.Candidates.AddAsync(candidate);
            }
        }

}
