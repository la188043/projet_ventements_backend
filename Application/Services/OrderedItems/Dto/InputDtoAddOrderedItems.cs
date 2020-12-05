using System.Collections.Generic;

namespace Application.Services.OrderedItems.Dto
{
    public class InputDtoAddOrderedItems
    {
        public IList<OrderedItem> OrderedItems { get; set; }
        
        public class OrderedItem
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }
}