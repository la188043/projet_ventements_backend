using System;
using Domain.Items;
using Domain.Users;

namespace Application.Services.Reviews.Dto
{
    public class OutputDtoQueryReview
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Title { get; set; }
        public string DescriptionReview { get; set; }
        public User Reviewer { get; set; }
        public Item ItemReviewed { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }

            private bool Equals(User other)
            {
                return Id == other.Id && Firstname == other.Firstname && Lastname == other.Lastname;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((User) obj);
            }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }

            private bool Equals(Item other)
            {
                return Id == other.Id && Label == other.Label;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((Item) obj);
            }
        }

        private bool Equals(OutputDtoQueryReview other)
        {
            return Id == other.Id && Stars == other.Stars && Title == other.Title &&
                   DescriptionReview == other.DescriptionReview && Reviewer.Equals(other.Reviewer) &&
                   ItemReviewed.Equals(other.ItemReviewed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OutputDtoQueryReview) obj);
        }
    }
}