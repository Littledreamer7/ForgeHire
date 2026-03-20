using ForgeHire.Data;
using ForgeHire.Models;
using ForgeHire.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
