namespace Application.Services.Addresses.Dto
{
    public class OutputDtoAddAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
    }
}