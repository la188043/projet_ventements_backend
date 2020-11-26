using Application.Services.Categories;
using Application.Services.Categories.Dto;
using Application.Services.SubCategories;
using Application.Services.SubCategories.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public ActionResult<OutputDtoQueryCategory> Query()
        {
            return Ok(_categoryService.Query());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<OutputDtoAddCategory> Post([FromBody] InputDtoAddCategory inputDtoAddCategory)
        {
            return Ok(_categoryService.Create(inputDtoAddCategory));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Update(int id, InputDtoUpdateCategory inputDtoUpdateCategory)
        {
            if (_categoryService.Update(id, inputDtoUpdateCategory))
            {
                return Ok();
            }

            return NotFound();
        }

        // SubCategories
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{categoryId:int}/subcategories")]
        public ActionResult<OutputDtoQuerySubCategory> AddSubCategory(int categoryId,
            [FromBody] InputDtoAddSubCategory subCategory)
        {
            return Ok(_subCategoryService.Create(categoryId, subCategory));
        }

        [HttpGet]
        [Route("{categoryId:int}/subcategories")]
        public ActionResult<OutputDtoQuerySubCategory> GetSubCategories(int categoryId)
        {
            return Ok(_subCategoryService.GetByCategoryId(categoryId));
        }
    }
}