using ForgeHire.Dtos.Company_Dtos;
using ForgeHire.Dtos.Company_Dtos.ForgeHire.Dtos.Company;
using ForgeHire.Helpers.Common;

public interface ICompanyService
{
    Task<ApiResponse> RegisterCompanyAsync(CompanySignUpDto dto);

    Task<CompanyProfileDto?> GetProfileAsync(string mobile);

    Task<CompanyProfileDto?> GetCompanyByIdAsync(int companyId);

    Task<ApiResponse> UpdateCompanyAsync(UpdateCompanyDto dto);

    Task<CompanyDashboardDto> GetCompanyDashboardAsync(int companyId);
}