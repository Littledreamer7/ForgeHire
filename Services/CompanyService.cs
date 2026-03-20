using ForgeHire.Data;
using ForgeHire.Dtos.Company_Dtos;
using ForgeHire.Dtos.Company_Dtos.ForgeHire.Dtos.Company;
using ForgeHire.Helpers;
using ForgeHire.Helpers.Common;
using ForgeHire.Models;
using ForgeHire.Models.Company_Model;
using ForgeHire.Models.User_Model;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _db;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyUserRepository _companyUserRepository;

        public CompanyService(
            AppDbContext db,
            ICompanyRepository companyRepository,
            IUserRepository userRepository,
            ICompanyUserRepository companyUserRepository)
        {
            _db = db;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _companyUserRepository = companyUserRepository;
        }

        // ======================
        // REGISTER COMPANY
        // ======================
        public async Task<ApiResponse> RegisterCompanyAsync(CompanySignUpDto request)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                // 🔥 NORMALIZE FIRST
                var mobile = PhoneHelper.NormalizeMobile(request.MobileNumber);

                if (!PhoneHelper.IsValidIndianMobile(mobile))
                    return new ApiResponse(false, "Invalid mobile number");

                // STEP 1: Check if company already exists
                var existingCompany = await _companyRepository.GetByMobile(mobile);

                if (existingCompany != null)
                    return new ApiResponse(false, "Company already exists");

                // STEP 2: Get or Create User
                var user = await _userRepository.GetByMobile(mobile);

                if (user == null)
                {
                    user = new User
                    {
                        MobileNumber = mobile,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _userRepository.AddAsync(user);
                    await _db.SaveChangesAsync();
                }

                // STEP 3: Create Company
                var company = new Company_Info
                {
                    CompanyName = request.CompanyName,
                    MobileNumber = mobile, // 🔥 USE NORMALIZED
                    GstNumber = request.GstNumber,
                    CinNumber = request.CinNumber,
                    Email = request.Email,
                    Location = request.Location,
                    IndustryType = request.IndustryType,
                    Website = request.Website,
                    CompanySize = request.CompanySize,
                    IsVerified = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _companyRepository.AddAsync(company);
                await _db.SaveChangesAsync();

                // STEP 4: Create CompanyUser mapping
                var companyUser = new CompanyUser
                {
                    UserId = user.Id,
                    CompanyId = company.Id,
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _companyUserRepository.AddAsync(companyUser);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ApiResponse(true, "Company registered successfully", new
                {
                    company.Id,
                    company.CompanyName
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ApiResponse(false, ex.Message);
            }
        }
        // ======================
        // GET PROFILE
        // ======================
        public async Task<CompanyProfileDto?> GetProfileAsync(string mobile)
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(x => x.MobileNumber == mobile);

            if (company == null)
                throw new ApplicationException("Company not found");

            return new CompanyProfileDto
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                MobileNumber = company.MobileNumber,
                GstNumber = company.GstNumber,
                CinNumber = company.CinNumber,
                Email = company.Email,
                Location = company.Location,
                IndustryType = company.IndustryType,
                Website = company.Website,
                CompanySize = company.CompanySize,
                IsVerified = company.IsVerified
            };
        }

        // ======================
        // GET COMPANY BY ID
        // ======================
        public async Task<CompanyProfileDto?> GetCompanyByIdAsync(int companyId)
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(x => x.Id == companyId);

            if (company == null)
                throw new ApplicationException("Company not found");

            return new CompanyProfileDto
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                MobileNumber = company.MobileNumber,
                GstNumber = company.GstNumber,
                CinNumber = company.CinNumber,
                Email = company.Email,
                Location = company.Location,
                IndustryType = company.IndustryType,
                Website = company.Website,
                CompanySize = company.CompanySize,
                IsVerified = company.IsVerified
            };
        }

        // ======================
        // UPDATE COMPANY
        // ======================
        public async Task<ApiResponse> UpdateCompanyAsync(UpdateCompanyDto dto)
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (company == null)
                throw new ApplicationException("Company not found");

            company.CompanyName = dto.CompanyName;
            company.GstNumber = dto.GstNumber;
            company.CinNumber = dto.CinNumber;
            company.Email = dto.Email;
            company.Location = dto.Location;
            company.IndustryType = dto.IndustryType;
            company.Website = dto.Website;
            company.CompanySize = dto.CompanySize;

            await _db.SaveChangesAsync();

            return new ApiResponse(true, "Company profile updated", null);
        }

        // ======================
        // COMPANY DASHBOARD
        // ======================
        public async Task<CompanyDashboardDto> GetCompanyDashboardAsync(int companyId)
        {
            var companyExists = await _db.Companies
                .AnyAsync(x => x.Id == companyId);

            if (!companyExists)
                throw new ApplicationException("Company not found");

            var totalJobs = await _db.Jobs
                .CountAsync(x => x.CompanyId == companyId); // ✅ FIXED

            var activeJobs = await _db.Jobs
                .CountAsync(x => x.CompanyId == companyId && x.Status == "Active");

            var totalCandidates = await _db.Candidates.CountAsync();

            return new CompanyDashboardDto
            {
                TotalJobs = totalJobs,
                ActiveJobs = activeJobs,
                TotalApplications = totalCandidates
            };
        }
    }
}