using Application.Services.Addresses.Dto;
using Application.Services.BaggedItems;
using Application.Services.BaggedItems.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBaggedItemService _baggedItemService;

        public UserController(IUserService userService, IBaggedItemService baggedItemService)
        {
            _userService = userService;
            _baggedItemService = baggedItemService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<OutputDtoQueryUser> Query()
        {
            return Ok(_userService.Query());
        }

        [HttpPost]
        public ActionResult<OutputDtoAuthenticateUser> Post(
            [FromBody] InputDtoAddUser user)
        {
            return Ok(_userService.Create(user));
        }

        [HttpPost]
        [Route("authenticate")]
        public ActionResult<OutputDtoAuthenticateUser> Authenticate([FromBody] InputDtoAuthenticateUser user)
        {
            var response = _userService.Authenticate(user);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }
        
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoQueryUser> Get(int id)
        {
            return Ok(_userService.GetById(id));
        }

        // Addresses
        [Authorize]
        [HttpPost]
        [Route("{idUser:int}/address")]
        public ActionResult RegisterAddress(int idUser, [FromBody] InputDtoAddAddress address)
        {
            if (_userService.RegisterAddress(idUser, address))
                return Ok();

            return NotFound();
        }
        
        // Bag
        [Authorize]
        [HttpGet]
        [Route("{userId:int}/bag")]
        public ActionResult<OutputDtoQueryUserBaggedItem> GetBagByUserId(int userId)
        {
            return Ok(_baggedItemService.GetByUserId(userId));
        }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}/bag/{itemId:int}")]
        public ActionResult<OutputDtoAddBaggedItem> AddToBag(int userId, int itemId,
            [FromBody] InputDtoAddItemToBag inputDtoAddItemToBag)
        {
            return Ok(_baggedItemService.AddToBag(userId, itemId, inputDtoAddItemToBag));
        }
    }
}