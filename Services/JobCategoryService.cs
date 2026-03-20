//using ForgeHire.Data;
//using ForgeHire.Dtos.Company_Dtos;
//using ForgeHire.Services.IServices;
//using Microsoft.EntityFrameworkCore;

//namespace ForgeHire.Services
//{
//    public class JobCategoryService : IJobCategoryService
//    {
//        private readonly AppDbContext _context;

//        public JobCategoryService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<JobCategoryDto>> GetAllCategoriesAsync()
//        {
//            return await _context.JobCategories
//                .Select(x => new JobCategoryDto
//                {
//                    Id = x.Id,
//                    Name = x.Name
//                })
//                .ToListAsync();
//        }
//    }
//}
