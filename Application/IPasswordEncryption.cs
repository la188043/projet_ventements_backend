using Domain.Users;

namespace Application
{
    public interface IPasswordEncryption
    {
        public string HashPassword(IUser user, string providedPassword);
        public bool VerifyPassword(IUser user, string hashedPassword, string providedPassword);
    }
}