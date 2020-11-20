using System;

namespace Application.Services.Users.Dto
{
    public class InputDtoAddUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; } // will encrypted then affected to EncryptedPassword
        public string Gender { get; set; }
    }
}