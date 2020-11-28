using System;
using Domain.Items;
using Domain.Users;

namespace Application.Services.Reviews.Dto
{
    public class OutputDtoQueryReview
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int Likes { get; set; }
        public string Title { get; set; }
        public string DescriptionReview { get; set; }
        public User Reviewer { get; set; }
        public Item ItemReviewed { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }
        }
    }
}