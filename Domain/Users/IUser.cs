using System;
using Domain.Addresses;
using Domain.Shared;

namespace Domain.Users
{
    public interface IUser : IEntity
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        DateTime Birthdate { get; set; }
        string Email { get; set; }
        string EncryptedPassword { get; set; }
        char Gender { get; set; }
        bool Administrator { get; set; }
        IAddress Address { get; set; }
    
    }
}