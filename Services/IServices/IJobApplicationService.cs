using ForgeHire.Dtos.Job_Application_Dtos;
using ForgeHire.Enums;
using ForgeHire.Models.Job_Application_Model;

namespace ForgeHire.Services.IServices
{
    public interface IJobApplicationService
    {
        Task ApplyJobAsync(int candidateId, ApplyJobDto dto, CancellationToken ct = default);

        Task<List<ApplicationResponseDto>> GetMyApplicationsAsync(int candidateId, CancellationToken ct = default);

        Task<List<JobApplication>> GetApplicantsByJobAsync(int jobId, CancellationToken ct = default);

        Task UpdateStatusAsync(int applicationId, ApplicationStatus status, CancellationToken ct = default);

        Task WithdrawAsync(int applicationId, int candidateId, CancellationToken ct = default);
    }
}