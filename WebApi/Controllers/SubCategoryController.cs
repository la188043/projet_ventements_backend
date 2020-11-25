using Application.Services.SubCategories;
using Application.Services.SubCategories.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
   
        [ApiController]
        [Route("api/subcategories")]
        public class SubCategoryController: ControllerBase
        {
            
           private readonly ISubCategoryService _subCategoryService;
           public SubCategoryController(ISubCategoryService subCategory)
           {
               _subCategoryService = subCategory;
           }
           
           // Query
           [HttpGet]
           public ActionResult<OutputDtoQuerySubCategory> Query()
           {
               return Ok(_subCategoryService.Query());
           }
           
           // Get
           [HttpGet]
           [Route("{id:int}")]
           public ActionResult<OutputDtoQuerySubCategory> Get(int id)
           {
               return Ok(_subCategoryService.GetById(id));
           }
         
          // GetByCategoryId
          [HttpGet("bycategoryid/{id}")]
          public ActionResult<OutputDtoQuerySubCategory> GetByCategoryId(int id)
          {
              var subcategoryDto = _subCategoryService.GetByCategoryId(id);
              return subcategoryDto != null ? (ActionResult<OutputDtoQuerySubCategory>)Ok(subcategoryDto) : NotFound();
          }
          
           // Post
           [HttpPost]
           public ActionResult<OutputDtoAddSubCategory> Post([FromBody] InputDtoAddSubCategory inputDtoAddSubCategory)
           {
               return Ok(_subCategoryService.Create(inputDtoAddSubCategory));
           }

           // Update
           [HttpPut]
           [Route("{id:int}")]
           public ActionResult Update(int id, InputDtoUpdateSubCategory inputDtoUpdateCategory)
           {
               if (_subCategoryService.Update(id, inputDtoUpdateCategory))
               {
                   return Ok();
               }
               return NotFound();
           }
           
        }
    
}