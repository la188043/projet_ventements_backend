using System;
using System.Collections.Generic;
using Domain.Items;
using Domain.Users;

namespace Application.Services.WishLists.Dto
{
    public class OutputDtoQueryWishLists
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public User UserWishList { get; set; }
        public Item ItemWishList { get; set; }


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
                if (obj.GetType() != this.GetType()) return false;
                return Equals((User) obj);
            }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public float Price { get; set; }
            public string ImageItem { get; set; }
            public string DescriptionItem { get; set; }

            private bool Equals(Item other)
            {
                return Id == other.Id && Label == other.Label && Price.Equals(other.Price) &&
                       ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Item) obj);
            }
        }

        private bool Equals(OutputDtoQueryWishLists other)
        {
            return Id == other.Id && UserWishList.Equals(other.UserWishList) &&
                   ItemWishList.Equals(other.ItemWishList);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoQueryWishLists) obj);
        }
    }
}