namespace Application.Services.Addresses.Dto
{
    public class InputDtoAddAddress
    {
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
    }
}