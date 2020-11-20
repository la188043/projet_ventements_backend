using System;

namespace Application.Services.Users.Dto
{
    public class OutputDtoQueryUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public bool Administrator { get; set; }
    }
}