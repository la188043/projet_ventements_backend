using System;
using Domain.Users;

namespace Application.Services.Users.Dto
{
    public class OutputDtoAuthenticateUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public bool Administrator { get; set; }
        public string Token { get; set; }

        public OutputDtoAuthenticateUser(IUser user, string token)
        {
            Id = user.Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Birthdate = user.Birthdate;
            Email = user.Email;
            Gender = user.Gender;
            Administrator = user.Administrator;
            Token = token;
        }
    }
}