using Application.Exceptions;
using Application.Services.OrderedItems;
using Application.Services.OrderedItems.Dto;
using Application.Services.Orders;
using Application.Services.Orders.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderedItemService _orderedItemService;

        public OrderController(IOrderService orderService, IOrderedItemService orderedItemService)
        {
            _orderService = orderService;
            _orderedItemService = orderedItemService;
        }

        // Orders
        [Authorize]
        [HttpGet]
        [Route("{orderId:int}")]
        public ActionResult<OutputDtoQueryOrder> GetById(int orderId)
        {
            return Ok(_orderService.GetById(orderId));
        }

        [Authorize]
        [HttpDelete]
        [Route("{orderId:int}")]
        public ActionResult Delete(int orderId)
        {
            if (_orderService.Delete(orderId))
                return Ok();

            return NotFound();
        }

        // OrderedItems
        [Authorize]
        [HttpPost]
        [Route("{orderId:int}/orderedItems/{itemId:int}")]
        public ActionResult<OutputDtoQueryOrderedItem> AddItemToOrder(int orderId,
            int itemId, [FromBody] InputDtoAddOrderedItem inputDtoAddOrderedItem)
        {
            try
            {
                var response = 
                    _orderedItemService.AddItemToOrder(orderId, itemId, inputDtoAddOrderedItem);
                
                return Ok(response);
            }
            catch (DuplicateSqlPrimaryException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [Authorize]
        [HttpPost]
        [Route("{orderId:int}/orderedItems")]
        public ActionResult<OutputDtoQueryOrderedItem> AddItemsToOrder(int orderId,
            [FromBody] InputDtoAddOrderedItems orderedItems)
        {
            return Ok(_orderedItemService.AddItemsToOrder(orderId, orderedItems));
        }
    }
}