using ForgeHire.Dtos.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace ForgeHire.Controllers
{
    [ApiController]
    [Route("api/candidate")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;

        public CandidateController(ICandidateService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(CandidateSignUpDto dto)
        {
            await _service.RegisterCandidateAsync(dto);
            return Ok(new { message = "Candidate registered successfully" });
        }
    }
}