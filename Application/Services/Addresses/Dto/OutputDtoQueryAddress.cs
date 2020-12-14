namespace Application.Services.Addresses.Dto
{
    public class OutputDtoQueryAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        
        // TEST
        public override bool Equals(object obj)
        {
            if (obj is OutputDtoQueryAddress address)
            {
                return
                    Id == address.Id &&
                    Street.Equals(address.Street) &&
                    HomeNumber == address.HomeNumber &&
                    Zip.Equals(address.Zip) &&
                    City.Equals(address.City);
            }

            return false;
        }
    }
}