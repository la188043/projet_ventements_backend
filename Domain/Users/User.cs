using System;
using Domain.Addresses;

namespace Domain.Users
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public char Gender { get; set; }
        public bool Administrator { get; set; }
        public IAddress Address { get; set; }

        public User()
        {
        }
    }
}