using System.Collections.Generic;
using Application.Repositories;
using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryption _passwordEncryption;

        public UserService(IUserRepository userRepository, IPasswordEncryption passwordEncryption)
        {
            _userRepository = userRepository;
            _passwordEncryption = passwordEncryption;
        }

        public IEnumerable<OutputDtoQueryUser> Query()
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoQueryUser GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoAuthenticateUser Create(InputDtoAddUser user)
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoAuthenticateUser Authenticate(InputDtoAuthenticateUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}