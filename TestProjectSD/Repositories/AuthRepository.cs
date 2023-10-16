using System.Security.Cryptography;
using TestProjectSD;
using TestProjectSD.Dtos;
using TestProjectSD.Helpers;
using TestProjectSD.Models;
using TestProjectSD.Repositories;
using TestProjectSD_withDatabase.Interface;

namespace TestProjectSD_withDatabase.Repositories
{
    public class AuthRepository : DataRepository, IAuthRepository
    {
        private readonly AuthHelper _authHelper;
        public AuthRepository(DataBaseContext context, IConfiguration config) : base(context)
        {
            _authHelper = new AuthHelper(config);
        }
        public Auth? GetSingleAuthByName(string name)
        {
            var existingUser = _dataContext.Auth.Where(u => u.Name == name).FirstOrDefault();

           return existingUser;
        }

        public void AddNewAuthData(UserForRegistrationDto userForRegistration)
        {
            byte[] passwordSalt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(userForRegistration.Password, passwordSalt);

            var newUserRegistration = new Auth();
            newUserRegistration.Name = userForRegistration.Name;
            newUserRegistration.PasswordHash = passwordHash;
            newUserRegistration.PasswordSalt = passwordSalt;

            AddEntity(newUserRegistration);

        }

    }
}
