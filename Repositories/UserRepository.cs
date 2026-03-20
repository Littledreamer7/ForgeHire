using ForgeHire.Data;
using ForgeHire.Models;
using ForgeHire.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByMobile(string mobile)
        {
            return await _db.Users
                .FirstOrDefaultAsync(x => x.MobileNumber == mobile);
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }
    }
}
