using ForgeHire.Dtos.Job_Dtos;
using ForgeHire.Helpers;
using ForgeHire.Helpers.AuthHelper;
using ForgeHire.Models.Job_Model;
using ForgeHire.Repositories.IRepositories;

namespace ForgeHire.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repository;
        private readonly TenantProvider _tenant;

        public JobService(IJobRepository repository, TenantProvider tenant)
        {
            _repository = repository;
            _tenant = tenant;
        }

        // ======================
        // COMMON MAPPER (CLEAN)
        // ======================
        private JobResponseDto Map(Job job)
        {
            return new JobResponseDto
            {
                Id = job.Id,
                Title = job.Title,
                Department = job.Department,
                Location = job.Location,
                Status = job.Status,
                ApplicantsCount = job.ApplicantsCount,
                PostedDate = job.PostedDate
            };
        }

        // ======================
        // GET ALL (TENANT SAFE)
        // ======================
        public async Task<IEnumerable<JobResponseDto>> GetAllJobs()
        {
            var companyId = _tenant.GetCompanyId();

            var jobs = await _repository.GetJobsByCompanyIdAsync(companyId);

            return jobs.Select(Map);
        }

        // ======================
        // GET BY ID (TENANT SAFE)
        // ======================
        public async Task<JobResponseDto> GetJobById(int id)
        {
            var companyId = _tenant.GetCompanyId();

            var job = await _repository.GetJobByIdAsync(id);

            if (job == null || job.CompanyId != companyId)
                throw new UnauthorizedAccessException("Unauthorized access");

            return Map(job);
        }

        // ======================
        // CREATE (ADMIN + HR ONLY)
        // ======================
        public async Task<JobResponseDto> CreateJob(CreateJobDto dto)
        {
            var companyId = _tenant.GetCompanyId();
            var role = _tenant.GetRole();

            //RBAC
            AuthHelper.EnsureAdminOrHR(role);

            var job = new Job
            {
                Title = dto.Title,
                Department = dto.Department,
                Location = dto.Location,
                JobType = dto.JobType,
                SalaryRange = dto.SalaryRange,
                Description = dto.Description,
                CompanyId = companyId
            };

            var result = await _repository.CreateJobAsync(job);

            return Map(result);
        }

        // ======================
        // UPDATE (ADMIN + HR)
        // ======================
        public async Task<JobResponseDto> UpdateJob(int id, UpdateJobDto dto)
        {
            var companyId = _tenant.GetCompanyId();
            var role = _tenant.GetRole();

            //RBAC
            AuthHelper.EnsureAdminOrHR(role);

            var job = await _repository.GetJobByIdAsync(id);

            if (job == null || job.CompanyId != companyId)
                throw new UnauthorizedAccessException("Unauthorized access");

            job.Title = dto.Title ?? job.Title;
            job.Department = dto.Department ?? job.Department;
            job.Location = dto.Location ?? job.Location;
            job.JobType = dto.JobType ?? job.JobType;
            job.SalaryRange = dto.SalaryRange ?? job.SalaryRange;
            job.Status = dto.Status ?? job.Status;
            job.Description = dto.Description ?? job.Description;

            await _repository.UpdateJobAsync(job);

            return Map(job);
        }

        // ======================
        // DELETE (ADMIN ONLY)
        // ======================
        public async Task<bool> DeleteJob(int id)
        {
            var companyId = _tenant.GetCompanyId();
            var role = _tenant.GetRole();

            //RBAC
            AuthHelper.EnsureAdmin(role);

            var job = await _repository.GetJobByIdAsync(id);

            if (job == null || job.CompanyId != companyId)
                throw new UnauthorizedAccessException("Unauthorized access");

            return await _repository.DeleteJobAsync(id);
        }
    }
}