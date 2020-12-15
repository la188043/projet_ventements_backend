using System;
using System.Collections.Generic;
using Domain.Items;

namespace Application.Services.BaggedItems.Dto
{
    public class OutputDtoQueryUserBaggedItem
    {
        public User BagOwner { get; set; }
        public float TotalPrice { get; set; }
        public IEnumerable<BaggedItem> Items { get; set; }

        public class BaggedItem
        {
            public int Id { get; set; }
            public DateTime AddedAt { get; set; }
            public int Quantity { get; set; }
            public string Size { get; set; }
            public Item BagItem { get; set; }

            public class Item
            {
                public int Id { get; set; }
                public string Label { get; set; }
                public float Price { get; set; }
                public string ImageItem { get; set; }
                public string DescriptionItem { get; set; }
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        protected bool Equals(OutputDtoQueryUserBaggedItem other)
        {
            return Equals(BagOwner, other.BagOwner) && TotalPrice.Equals(other.TotalPrice) && Equals(Items, other.Items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoQueryUserBaggedItem) obj);
        }
    }
}