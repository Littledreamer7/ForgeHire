using ForgeHire.Dtos.Job_Application_Dtos;
using ForgeHire.Enums;
using ForgeHire.Helpers.Common;
using ForgeHire.Helpers.Common.ForgeHire.Helpers.Common;
using ForgeHire.Models.Job_Application_Model;
using ForgeHire.Repositories.IRepositories;
using ForgeHire.Services.IServices;
using Microsoft.Extensions.Logging;

namespace ForgeHire.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repo;
        private readonly ILogger<JobApplicationService> _logger;

        public JobApplicationService(
            IJobApplicationRepository repo,
            ILogger<JobApplicationService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task ApplyJobAsync(int candidateId, ApplyJobDto dto, CancellationToken ct = default)
        {
            if (await _repo.ExistsAsync(dto.JobId, candidateId, ct))
                throw new AppException("You have already applied for this job", 409);

            var application = new JobApplication
            {
                JobId = dto.JobId,
                CandidateId = candidateId,
                ResumeUrl = dto.ResumeUrl,
                Status = ApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(application, ct);
            await _repo.SaveAsync(ct);

            _logger.LogInformation("User {UserId} applied for Job {JobId}", candidateId, dto.JobId);
        }

        public async Task<List<ApplicationResponseDto>> GetMyApplicationsAsync(int candidateId, CancellationToken ct = default)
        {
            var apps = await _repo.GetByCandidateAsync(candidateId, ct);

            return apps.Select(x => new ApplicationResponseDto
            {
                Id = x.Id,
                JobTitle = x.Job.Title,
                CompanyName = x.Job.Company.CompanyName,
                Status = x.Status.ToString(),
                AppliedAt = x.AppliedAt
            }).ToList();
        }

        public async Task<List<JobApplication>> GetApplicantsByJobAsync(int jobId, CancellationToken ct = default)
        {
            return await _repo.GetByJobAsync(jobId, ct);
        }

        public async Task UpdateStatusAsync(int applicationId, ApplicationStatus status, CancellationToken ct = default)
        {
            var app = await _repo.GetByIdAsync(applicationId, ct)
                ?? throw new AppException("Application not found", 404);

            app.Status = status;

            await _repo.SaveAsync(ct);
        }

        public async Task WithdrawAsync(int applicationId, int candidateId, CancellationToken ct = default)
        {
            var app = await _repo.GetByIdAsync(applicationId, ct)
                ?? throw new AppException("Application not found", 404);

            if (app.CandidateId != candidateId)
                throw new AppException("Unauthorized", 403);

            app.Status = ApplicationStatus.Withdrawn;

            await _repo.SaveAsync(ct);
        }
    }
}