using Application.Services.Categories;
using Application.Services.Categories.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        
    }
}