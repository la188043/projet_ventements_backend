using System;
using System.Collections.Generic;

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
        }
    }
}