using ForgeHire.Dtos.Job_Dtos;
using ForgeHire.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForgeHire.Controllers.Jobs_Controller
{
  
        [ApiController]
        [Route("api/jobs")]
        [Authorize(Roles = "Admin,HR")]
    public class JobController : ControllerBase
        {
            private readonly IJobService _service;

            public JobController(IJobService service)
            {
                _service = service;
            }

        // ======================
        // GET ALL
        // ======================
        [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var jobs = await _service.GetAllJobs();
                return Ok(jobs);
            }

            // ======================
            // GET BY ID
            // ======================
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var job = await _service.GetJobById(id);
                return Ok(job);
            }

            // ======================
            // CREATE
            // ======================
            [HttpPost]
            public async Task<IActionResult> Create(CreateJobDto dto)
            {
                var job = await _service.CreateJob(dto);
                return Ok(job);
            }

            // ======================
            // UPDATE
            // ======================
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, UpdateJobDto dto)
            {
                var job = await _service.UpdateJob(id, dto);
                return Ok(job);
            }

            // ======================
            // DELETE
            // ======================
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _service.DeleteJob(id);
                return Ok(new { message = "Job deleted successfully" });
            }
        }
}