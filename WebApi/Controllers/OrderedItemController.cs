using Application.Services.OrderedItems;
using Application.Services.OrderedItems.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/orderedItems")]
    public class OrderedItemController : ControllerBase
    {
        private readonly IOrderedItemService _orderedItemService;

        public OrderedItemController(IOrderedItemService orderedItemService)
        {
            _orderedItemService = orderedItemService;
        }

        [Authorize]
        [HttpPut]
        [Route("{orderedItemId:int}")]
        public ActionResult UpdateQuantity(int orderedItemId,
            [FromBody] InputDtoUpdateOrderedItem inputDtoUpdateOrderedItem)
        {
            if (_orderedItemService.UpdateQuantity(orderedItemId, inputDtoUpdateOrderedItem))
                return Ok();

            return NotFound();
        }

        [Authorize]
        [HttpDelete]
        [Route("{orderedItemId:int}")]
        public ActionResult Delete(int orderedItemId)
        {
            if (_orderedItemService.Delete(orderedItemId))
                return Ok();

            return NotFound();
        }
    }
}