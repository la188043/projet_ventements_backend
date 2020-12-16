using System;

namespace Application.Services.Orders.Dto
{
    public class OutputDtoAddOrder
    {
        public int Id { get; set; }
        public bool isPaid { get; set; }
        public DateTime orderedAt { get; set; }

        private bool Equals(OutputDtoAddOrder other)
        {
            return Id == other.Id && isPaid == other.isPaid;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoAddOrder) obj);
        }
    }
}