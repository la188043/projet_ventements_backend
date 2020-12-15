using System;

namespace Application.Services.Addresses.Dto
{
    public class OutputDtoQueryAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        private bool Equals(OutputDtoQueryAddress other)
        {
            return Id == other.Id && Street == other.Street && HomeNumber == other.HomeNumber && Zip == other.Zip && City == other.City;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is OutputDtoQueryAddress other && Equals(other);
        }
    }
}