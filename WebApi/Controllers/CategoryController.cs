using System;
using Application.Services.Categories;
using Application.Services.Categories.Dto;
using Application.Services.Items;
using Application.Services.Items.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;

        public CategoryController(ICategoryService categoryService, IItemService itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
        }

        // Response : each parent category with its child categories
        [HttpGet]
        public ActionResult<OutputDtoQueryCategory> Query()
        {
            return Ok(_categoryService.Query());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoQueryCategory> Get(int id)
        {
            var response = _categoryService.GetById(id);

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<OutputDtoAddCategory> CreateCategory([FromBody] InputDtoAddCategory inputDtoAddCategory)
        {
            return Ok(_categoryService.CreateCategory(inputDtoAddCategory));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{categoryId:int}/subcategories")]
        public ActionResult<OutputDtoAddCategory> CreateSubCategory(int categoryId,
            [FromBody] InputDtoAddCategory subCategory)
        {
            return Ok(_categoryService.CreateSubCategory(categoryId, subCategory));
        }

        // items
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{subcategoryId:int}/items")]
        public ActionResult<OutputDtoQueryItem> AddItem(int subcategoryId,
            [FromBody] InputDtoAddItem item)
        {
            return Ok(_itemService.Create(subcategoryId, item));
        }

        [HttpGet]
        [Route("{subcategoryId:int}/items")]
        public ActionResult<OutputDtoQueryItem> GetItems(int subcategoryId)
        {
            return Ok(_itemService.GetByCategoryId(subcategoryId));
        }
    }
}