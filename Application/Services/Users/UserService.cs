using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Repositories;
using Application.Services.Users.Dto;
using Domain.Users;
using Microsoft.IdentityModel.Tokens;

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
            return _userRepository
                .Query()
                .Select(user => new OutputDtoQueryUser
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Birthdate = user.Birthdate,
                    Email = user.Email,
                    Gender = user.Gender,
                    Administrator = user.Administrator
                });
        }

        public OutputDtoQueryUser GetById(int id)
        {
            var user = _userRepository.GetById(id);

            return new OutputDtoQueryUser
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Gender = user.Gender,
                Administrator = user.Administrator
            };
        }

        public OutputDtoAuthenticateUser Create(InputDtoAddUser user)
        {
            var userFromDb = _userRepository.Create(new User
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                Email = user.Email,
                EncryptedPassword = _passwordEncryption.HashPassword(new User(), user.UserPassword),
                Gender = user.Gender[0]
            });

            var token = GenerateJwtToken(userFromDb);

            return new OutputDtoAuthenticateUser(userFromDb, token);
        }

        public OutputDtoAuthenticateUser Authenticate(InputDtoAuthenticateUser user)
        {
            var userFromDb = _userRepository.Authenticate(new User {Email = user.Email});

            bool passwordVerified =
                _passwordEncryption.VerifyPassword(
                    userFromDb, userFromDb.EncryptedPassword, user.PasswordUser);

            if (!passwordVerified)
                return null;

            var token = GenerateJwtToken(userFromDb);
            
            return new OutputDtoAuthenticateUser(userFromDb, token);
        }

        public string GenerateJwtToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}