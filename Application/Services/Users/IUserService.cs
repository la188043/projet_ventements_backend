using System.Collections.Generic;
using Application.Services.Addresses.Dto;
using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public interface IUserService
    {
        IEnumerable<OutputDtoQueryUser> Query();
        OutputDtoQueryUser GetById(int id);
        OutputDtoAuthenticateUser Create(InputDtoAddUser user);
        OutputDtoAuthenticateUser Authenticate(InputDtoAuthenticateUser user);
        OutputDtoQueryAddress RegisterAddress(InputDtoAddAddress address);
    }
}