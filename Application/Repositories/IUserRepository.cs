using System.Collections.Generic;
using Application.Services.Addresses.Dto;
using Domain.Addresses;
using Domain.Users;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Query();
        IUser GetById(int id);
        IUser Create(IUser user);
        IUser Authenticate(IUser user);
        bool RegisterAddress(int idUser, IAddress address);
        IAddress GetUserAddress(int idUser);
    }
}