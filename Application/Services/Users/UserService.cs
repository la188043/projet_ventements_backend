using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Addresses;
using Application.Services.Addresses.Dto;
using Application.Services.Users.Dto;
using Domain.Addresses;
using Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressService _addressService;
        private readonly IPasswordEncryption _passwordEncryption;

        public UserService(IUserRepository userRepository, IAddressService addressService,
            IPasswordEncryption passwordEncryption)
        {
            _userRepository = userRepository;
            _addressService = addressService;
            _passwordEncryption = passwordEncryption;
        }

        public IEnumerable<OutputDtoQueryUser> Query()
        {
            return _userRepository
                .Query()
                .Select(user =>
                {
                    var userAddress = user.Address != null
                        ? new OutputDtoQueryUser.Address
                        {
                            Id = user.Address.Id,
                            Street = user.Address.Street,
                            HomeNumber = user.Address.HomeNumber,
                            Zip = user.Address.Zip,
                            City = user.Address.City
                        }
                        : null;


                    return new OutputDtoQueryUser
                    {
                        Id = user.Id,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Birthdate = user.Birthdate,
                        Email = user.Email,
                        Gender = user.Gender,
                        Administrator = user.Administrator,
                        UserAddress = userAddress
                    };
                });
        }

        public OutputDtoQueryUser GetById(int id)
        {
            var user = _userRepository.GetById(id);

            var userAddress = user.Address != null
                ? new OutputDtoQueryUser.Address
                {
                    Id = user.Address.Id,
                    Street = user.Address.Street,
                    HomeNumber = user.Address.HomeNumber,
                    Zip = user.Address.Zip,
                    City = user.Address.City
                }
                : null;

            return new OutputDtoQueryUser
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthdate = user.Birthdate,
                Email = user.Email,
                Gender = user.Gender,
                Administrator = user.Administrator,
                UserAddress = userAddress
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
                EncryptedPassword = _passwordEncryption.HashPassword(new User(), user.PasswordUser),
                Gender = user.Gender[0]
            });

            var token = GenerateJwtToken(userFromDb);

            return new OutputDtoAuthenticateUser(userFromDb, token);
        }

        public OutputDtoAuthenticateUser Authenticate(InputDtoAuthenticateUser user)
        {
            var userFromDb = _userRepository.Authenticate(new User {Email = user.Email});

            if (userFromDb == null)
                throw new NullUserException();

            bool passwordVerified =
                _passwordEncryption.VerifyPassword(
                    userFromDb, userFromDb.EncryptedPassword, user.PasswordUser);

            if (!passwordVerified)
                throw new WrongPasswordException();

            var token = GenerateJwtToken(userFromDb);

            return new OutputDtoAuthenticateUser(userFromDb, token);
        }

        public bool Delete(int userId)
        {
            return _userRepository.Delete(userId);
        }

        public OutputDtoQueryAddress RegisterAddress(int idUser, InputDtoAddAddress address)
        {
            // Si null on le créé (?? = nullish operator)
            var addressChecked = _addressService.CheckFromDb(address) ?? _addressService.Create(address);

            var addressFromDb = _userRepository.RegisterAddress(idUser, new Address {Id = addressChecked.Id});

            if (!addressFromDb)
            {
                throw new CouldNotUpdateAddressException();
            }

            return new OutputDtoQueryAddress
            {
                Id = addressChecked.Id,
                Street = addressChecked.Street,
                HomeNumber = addressChecked.HomeNumber,
                Zip = addressChecked.Zip,
                City = addressChecked.City,
            };
        }

        public OutputDtoQueryAddress GetUserAddress(int idUser)
        {
            var addressFromDb = _userRepository.GetUserAddress(idUser);

            return new OutputDtoQueryAddress
            {
                Id = addressFromDb.Id,
                Street = addressFromDb.Street,
                HomeNumber = addressFromDb.HomeNumber,
                Zip = addressFromDb.Zip,
                City = addressFromDb.City
            };
        }

        public string GenerateJwtToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);

            // Token claims
            IList<Claim> tokenClaims = new List<Claim>();
            tokenClaims.Add(new Claim("id", user.Id.ToString()));
            if (user.Administrator)
            {
                tokenClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}