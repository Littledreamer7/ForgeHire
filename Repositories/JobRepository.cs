using ForgeHire.Data;
using ForgeHire.Models.Job_Model;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        // ======================
        // GET ALL (PUBLIC)
        // ======================
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs
                .AsNoTracking()
                .OrderByDescending(x => x.PostedDate)
                .ToListAsync();
        }

        // ======================
        // GET BY ID
        // ======================
        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _context.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // ======================
        // GET BY COMPANY (TENANT)
        // ======================
        public async Task<IEnumerable<Job>> GetJobsByCompanyIdAsync(int companyId)
        {
            return await _context.Jobs
                .Where(x => x.CompanyId == companyId)
                .AsNoTracking()
                .OrderByDescending(x => x.PostedDate)
                .ToListAsync();
        }

        // ======================
        // CREATE
        // ======================
        public async Task<Job> CreateJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            return job;
        }

        // ======================
        // UPDATE
        // ======================
        public async Task UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        // ======================
        // DELETE
        // ======================
        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
                return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}