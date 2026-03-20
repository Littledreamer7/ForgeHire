using ForgeHire.Dtos.Job_Application_Dtos;
using ForgeHire.Enums;
using ForgeHire.Helpers.Common;
using ForgeHire.Helpers.Common.ForgeHire.Helpers.Common;
using ForgeHire.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForgeHire.Controllers
{
    [ApiController]
    [Route("api/applications")]
    [Authorize]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new AppException("Invalid token", 401);

            return int.Parse(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Apply([FromBody] ApplyJobDto dto, CancellationToken ct)
        {
            await _service.ApplyJobAsync(GetUserId(), dto, ct);

            return Ok(ApiResponse.SuccessResponse("Application submitted successfully"));
        }

        [HttpGet("my")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GetMyApplications(CancellationToken ct)
        {
            var result = await _service.GetMyApplicationsAsync(GetUserId(), ct);

            return Ok(ApiResponse.SuccessResponse("Fetched successfully", result));
        }

        [HttpPut("{id}/withdraw")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Withdraw(int id, CancellationToken ct)
        {
            await _service.WithdrawAsync(id, GetUserId(), ct);

            return Ok(ApiResponse.SuccessResponse("Application withdrawn"));
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] ApplicationStatus status, CancellationToken ct)
        {
            await _service.UpdateStatusAsync(id, status, ct);

            return Ok(ApiResponse.SuccessResponse("Status updated"));
        }
    }
}