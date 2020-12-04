using System;

namespace Application.Services.Orders.Dto
{
    public class OutputAddOrder
    {
        public int Id { get; set; }
        public bool isPaid { get; set; }
        public DateTime orderedAt { get; set; }
    }
}