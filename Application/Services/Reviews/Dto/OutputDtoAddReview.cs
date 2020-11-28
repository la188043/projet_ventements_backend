using System;
using Domain.Items;
using Domain.Users;

namespace Application.Services.Reviews.Dto
{
    public class OutputDtoAddReview
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int Likes { get; set; }
        public string Title { get; set; }
        public string DescriptionReview { get; set; }
        public IUser User { get; set; }
        public IItem Item { get; set; }
    }
}