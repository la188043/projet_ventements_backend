using Application.Services.Reviews;
using Application.Services.Reviews.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController: ControllerBase
    {
        private readonly IReviewService _reviewService;
       
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("{uservId:int}/item/{itemId:int}")]
        public ActionResult<OutputDtoAddReview> Post(int uservId,int itemId,[FromBody] InputDtoAddReview inputDtoAddReview)
        {
            return Ok(_reviewService.Create(uservId,itemId,inputDtoAddReview));
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeleteReview( int id,int userId)
        {
            if (_reviewService.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
        
        
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Update(int id, InputDtoUpdateReview inputDtoUpdateReview)
        {
            if (_reviewService.Update(id, inputDtoUpdateReview))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}