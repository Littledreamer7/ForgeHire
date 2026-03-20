using ForgeHire.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ForgeHire.Controllers
{
    [ApiController]
    [Route("api/job-categories")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryService _categoryService;

        public JobCategoryController(IJobCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{
        //    var result = await _categoryService.GetAllCategoriesAsync();

        //    return Ok(result);
        //}
    }
}
