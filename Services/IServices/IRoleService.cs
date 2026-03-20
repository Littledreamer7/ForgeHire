using ForgeHire.Models;

namespace ForgeHire.Services.IServices
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoles();
    }
}
