using Application.Services.Items;
using Application.Services.Items.Dto;
using Application.Services.Reviews;
using Application.Services.Reviews.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IReviewService _reviewService;

        public ItemController(IItemService itemService, IReviewService reviewService)
        {
            _itemService = itemService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult<OutputDtoQueryItem> Query()
        {
            return Ok(_itemService.Query());
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoQueryItem> Get(int id)
        {
            return Ok(_itemService.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Update(int id, [FromBody] InputDtoUpdateItem inputDtoUpdateItem)
        {
            var response = _itemService.Update(id, inputDtoUpdateItem);

            if (response)
                return Ok();

            return NotFound();
        }
        
        // Reviews
        [HttpGet]
        [Route("{itemId:int}/reviews")]
        public ActionResult<OutputDtoQueryReview> GetReviewsByItemId(int itemId)
        {
            return Ok(_reviewService.GetByItemId(itemId));
        }
    }
}