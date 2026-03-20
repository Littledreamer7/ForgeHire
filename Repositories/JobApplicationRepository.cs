using ForgeHire.Data;
using ForgeHire.Models.Job_Application_Model;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _context;

        public JobApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int jobId, int candidateId, CancellationToken ct = default)
        {
            return await _context.JobApplications
                .AsNoTracking()
                .AnyAsync(x => x.JobId == jobId && x.CandidateId == candidateId, ct);
        }

        public async Task AddAsync(JobApplication application, CancellationToken ct = default)
        {
            await _context.JobApplications.AddAsync(application, ct);
        }

        public async Task<List<JobApplication>> GetByCandidateAsync(int candidateId, CancellationToken ct = default)
        {
            return await _context.JobApplications
                .AsNoTracking()
                .Include(x => x.Job)
                    .ThenInclude(j => j.Company)
                .Where(x => x.CandidateId == candidateId)
                .OrderByDescending(x => x.AppliedAt)
                .ToListAsync(ct);
        }

        public async Task<List<JobApplication>> GetByJobAsync(int jobId, CancellationToken ct = default)
        {
            return await _context.JobApplications
                .AsNoTracking()
                .Include(x => x.Candidate)
                .Include(x => x.Job)
                    .ThenInclude(j => j.Company)
                .Where(x => x.JobId == jobId)
                .OrderByDescending(x => x.AppliedAt)
                .ToListAsync(ct);
        }

        public async Task<JobApplication?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.JobApplications
                .Include(x => x.Job)
                    .ThenInclude(j => j.Company)
                .Include(x => x.Candidate)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task SaveAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}