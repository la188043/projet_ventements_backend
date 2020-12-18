using Application.Services.BaggedItems;
using Application.Services.BaggedItems.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/baggedItems")]
    public class BaggedItemController : ControllerBase
    {
        private readonly IBaggedItemService _baggedItemService;

        public BaggedItemController(IBaggedItemService baggedItemService)
        {
            _baggedItemService = baggedItemService;
        }

        [Authorize]
        [HttpDelete]
        [Route("{baggedItemId:int}")]
        public ActionResult DeleteItem(int baggedItemId)
        {
            if (_baggedItemService.DeleteItem(baggedItemId))
                return Ok();
            
            return NotFound();
        }

        [Authorize]
        [HttpPut]
        [Route("{baggedItemId:int}")]
        public ActionResult UpdateQuantity(int baggedItemId, [FromBody] InputDtoUpdateBaggedItem inputDtoUpdateBaggedItem)
        {
            if (_baggedItemService.UpdateQuantity(baggedItemId, inputDtoUpdateBaggedItem))
                return Ok();

            return NotFound();
        }
    }
}