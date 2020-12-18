using System;

namespace Application.Services.Orders.Dto
{
    public class OutputDtoAddOrder
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderedAt { get; set; }

        private bool Equals(OutputDtoAddOrder other)
        {
            return Id == other.Id && IsPaid == other.IsPaid;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OutputDtoAddOrder) obj);
        }
    }
}