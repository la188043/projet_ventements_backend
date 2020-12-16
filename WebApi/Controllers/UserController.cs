using System;
using Application.Exceptions;
using Application.Services.Addresses.Dto;
using Application.Services.BaggedItems;
using Application.Services.BaggedItems.Dto;
using Application.Services.Orders;
using Application.Services.Orders.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Application.Services.WishLists;
using Application.Services.WishLists.Dto;
using Domain.Exceptions;
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
        private readonly IWishListService _wishListService;
        private readonly IOrderService _orderService;

        public UserController(IUserService userService, IBaggedItemService baggedItemService,
            IWishListService wishListService, IOrderService orderService)
        {
            _userService = userService;
            _baggedItemService = baggedItemService;
            _wishListService = wishListService;
            _orderService = orderService;
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
            try
            {
                var response = _userService.Create(user);
                
                return Ok(response);
            }
            catch (DuplicateSqlPrimaryException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpPost]
        [Route("authenticate")]
        public ActionResult<OutputDtoAuthenticateUser> Authenticate([FromBody] InputDtoAuthenticateUser user)
        {
            try
            {
                var response = _userService.Authenticate(user);

                return Ok(response);
            }
            catch (NullUserException e)
            {
                return BadRequest(new {message = e.Message});
            }
            catch (WrongPasswordException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoQueryUser> Get(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_userService.Delete(id))
                return Ok();

            return NotFound();
        }

        // Addresses
        [Authorize]
        [HttpPost]
        [Route("{idUser:int}/address")]
        public ActionResult<OutputDtoQueryAddress> RegisterAddress(int idUser, [FromBody] InputDtoAddAddress address)
        {
            try
            {
                var response= _userService.RegisterAddress(idUser, address);
                
                return Ok(response);
            }
            catch (CouldNotUpdateAddressException e)
            {
                return BadRequest(new {mesage = e.Message});
            }
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
            try
            {
                var response = _baggedItemService.AddToBag(userId, itemId, inputDtoAddItemToBag);

                return Ok(response);
            }
            catch (DuplicateSqlPrimaryException e)
            {
                return NotFound(new {message = e.Message});
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{userId:int}/bag/empty")]
        public ActionResult<int> EmptyBag(int userId)
        {
            return Ok(_baggedItemService.EmptyBag(userId));
        }

        // Wishlist
        [Authorize]
        [HttpPost]
        [Route("{uservId:int}/wishlist/{itemId:int}")]
        public ActionResult<OutputDtoQueryWishLists> AddItemToWishlist(int uservId, int itemId)
        {
            try
            {
                var response = _wishListService.Add(uservId, itemId);

                return Ok(response);
            }
            catch (DuplicateSqlPrimaryException e)
            {
                return BadRequest(new {message = e.Message});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{uservId:int}/wishlist")]
        public ActionResult<OutputDtoQueryWishLists> QueryWishlist(int uservId)
        {
            return Ok(_wishListService.GetByUserId(uservId));
        }
        
        // Orders
        [Authorize]
        [HttpGet]
        [Route("{userId:int}/orders")]
        public ActionResult<OutputDtoQueryOrder> GetUserOrders(int userId)
        {
            return Ok(_orderService.GetByUserId(userId));
        }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}/orders")]
        public ActionResult<OutputDtoAddOrder> Create(int userId)
        {
            return Ok(_orderService.Create(userId));
        }
    }
}