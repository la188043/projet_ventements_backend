using Application.Services.Items;
using Application.Services.Items.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoQueryItem> Get(int id)
        {
            return Ok(_itemService.GetById(id));
        }
    }
}