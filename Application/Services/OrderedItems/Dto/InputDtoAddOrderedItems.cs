using System.Collections.Generic;

namespace Application.Services.OrderedItems.Dto
{
    public class InputDtoAddOrderedItems
    {
        public IEnumerable<OrderedItem> OrderedItems { get; set; }
        
        public class OrderedItem
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }
}