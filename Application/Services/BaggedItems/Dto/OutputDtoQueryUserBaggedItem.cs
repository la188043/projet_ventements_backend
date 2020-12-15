using System;
using System.Collections.Generic;
using System.Linq;

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

                private bool Equals(Item other)
                {
                    return Id == other.Id && Label == other.Label && Price.Equals(other.Price) && ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem;
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != this.GetType()) return false;
                    return Equals((Item) obj);
                }
            }

            private bool Equals(BaggedItem other)
            {
                return Id == other.Id && Quantity == other.Quantity && Size == other.Size && BagItem.Equals(other.BagItem);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((BaggedItem) obj);
            }
        }

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

        private bool Equals(OutputDtoQueryUserBaggedItem other)
        {
            return BagOwner.Equals(other.BagOwner) && TotalPrice.Equals(other.TotalPrice) && ListEquals(Items, other.Items);
        }

        private bool ListEquals(IEnumerable<BaggedItem> items, IEnumerable<BaggedItem> otherItems)
        {
            var itemsList = items.ToList();
            var otherItemsList = otherItems.ToList();

            return itemsList.Count == otherItemsList.Count && itemsList.All(item => otherItemsList.Contains(item));
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