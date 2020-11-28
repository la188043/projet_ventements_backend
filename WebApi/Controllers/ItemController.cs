using Application.Services.Items;
using Application.Services.Items.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

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

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Update(int id, InputDtoUpdateItem inputDtoUpdateItem)
        {
            var response = _itemService.Update(id, inputDtoUpdateItem);

            if (response)
                return Ok();

            return NotFound();
        }
    }
}