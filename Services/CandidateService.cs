using ForgeHire.Data;
using ForgeHire.Dtos.Candidate;
using ForgeHire.Models;
using ForgeHire.Models.User_Model;
using ForgeHire.Repositories.IRepositories;

namespace ForgeHire.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _db;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUserRepository _userRepository;

        public CandidateService(
            AppDbContext db,
            ICandidateRepository candidateRepository,
            IUserRepository userRepository)
        {
            _db = db;
            _candidateRepository = candidateRepository;
            _userRepository = userRepository;
        }

        public async Task RegisterCandidateAsync(CandidateSignUpDto dto)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                // STEP 1: CHECK EXISTING
                var existing = await _candidateRepository.GetByMobile(dto.MobileNumber);
                if (existing != null)
                    throw new Exception("Candidate already exists");

                // STEP 2: GET OR CREATE USER
                var user = await _userRepository.GetByMobile(dto.MobileNumber);

                if (user == null)
                {
                    user = new User
                    {
                        MobileNumber = dto.MobileNumber,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _userRepository.AddAsync(user);
                    await _db.SaveChangesAsync();
                }

                // STEP 3: CREATE CANDIDATE
                var candidate = new Candidate
                {
                    UserId = user.Id,
                    MobileNumber = dto.MobileNumber,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Location = dto.Location,
                    CreatedAt = DateTime.UtcNow
                };

                await _candidateRepository.AddAsync(candidate);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}