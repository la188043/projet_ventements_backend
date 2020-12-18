using Application.Services.WishLists;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
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