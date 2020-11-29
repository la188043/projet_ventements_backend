﻿using Application.Services.Users;
using Application.Services.WishLists;
using Application.Services.WishLists.Dto;
using Domain.Wishlists;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace VenteMine.Controllers
{
    [ApiController]
    [Route("api/wishlists")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishlistService;
        
        public WishListController(IWishListService wishListService)
        {
                _wishlistService = wishListService;
        }
        
        [HttpGet]
        public ActionResult<OutputDtoQueryWishLists> Query()
        {
            return Ok(_wishlistService.Query());
        }
        [Authorize]
        [HttpPost]
        [Route("{uservId:int}/item/{itemId:int}")]
        public ActionResult<OutputDtoQueryWishLists> Post(int uservId, int itemId,
            [FromBody]InputDtoAddWishList inputDtoAddWishList)
        {
            return Ok(_wishlistService.Add(uservId, itemId, inputDtoAddWishList));
        }
        
      
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeleteWishlist(int id)
        {
            if (_wishlistService.Delete(id))
            {
                return Ok();
            }

            return NotFound();
        }

    }
}