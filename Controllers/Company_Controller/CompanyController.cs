using ForgeHire.Dtos.Company_Dtos;
using ForgeHire.Helpers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForgeHire.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Register a new company
        /// </summary>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CompanySignUpDto dto)
        {
            var result = await _companyService.RegisterCompanyAsync(dto);

            return Ok(result);
        }

        /// <summary>
        /// Get company profile by mobile
        /// </summary>
        [HttpGet("profile/{mobile}")]
        public async Task<IActionResult> GetProfile(string mobile)
        {
            var profile = await _companyService.GetProfileAsync(mobile);

            if (profile == null)
            {
                return NotFound(new ApiResponse(
                    false,
                    "Company profile not found",
                    null
                ));
            }

            return Ok(new ApiResponse(
                true,
                "Company profile retrieved successfully",
                profile
            ));
        }

        /// <summary>
        /// Get company by Id
        /// </summary>
        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompany(int companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);

            return Ok(new ApiResponse(
                true,
                "Company retrieved successfully",
                company
            ));
        }

        /// <summary>
        /// Update company profile
        /// </summary>
        [HttpPut("update-company")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyDto dto)
        {
            var result = await _companyService.UpdateCompanyAsync(dto);

            return Ok(result);
        }

        ///// <summary>
        ///// Company dashboard stats
        ///// </summary>
        [HttpGet("dashboard/{companyId}")]
        public async Task<IActionResult> Dashboard(int companyId)
        {
            var stats = await _companyService.GetCompanyDashboardAsync(companyId);

            return Ok(new ApiResponse(
                true,
                "Dashboard stats retrieved",
                stats
            ));
        }
    }
}
