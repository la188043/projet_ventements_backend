using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Orders.Dto
{
    public class OutputDtoQueryOrder
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderedAt { get; set; }
        public float TotalPrice { get; set; }
        public User Ordered { get; set; }
        public IEnumerable<Item> OrderedItems { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }

            private bool Equals(User other)
            {
                return Id == other.Id && Firstname == other.Firstname && Lastname == other.Lastname &&
                       Email == other.Email;
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
            public int ItemId { get; set; }
            public string Label { get; set; }
            public float Price { get; set; }
            public string ImageItem { get; set; }
            public string DescriptionItem { get; set; }
            public int Quantity { get; set; }
            public string Size { get; set; }

            private bool Equals(Item other)
            {
                return Id == other.Id && ItemId == other.ItemId && Label == other.Label && Price.Equals(other.Price) &&
                       ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem &&
                       Quantity == other.Quantity && Size == other.Size;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((Item) obj);
            }
        }

        private bool Equals(OutputDtoQueryOrder other)
        {
            return Id == other.Id && IsPaid == other.IsPaid && TotalPrice.Equals(other.TotalPrice) &&
                   Ordered.Equals(other.Ordered) && ListEquals(OrderedItems, other.OrderedItems);
        }

        private bool ListEquals(IEnumerable<Item> orderedItems, IEnumerable<Item> otherOrderedItems)
        {
            var list = orderedItems.ToList();
            var other = otherOrderedItems.ToList();

            return list.Count == other.Count && list.All(item => other.Contains(item));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OutputDtoQueryOrder) obj);
        }
    }
}