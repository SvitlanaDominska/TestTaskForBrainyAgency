using TestProjectSD.Dtos;
using TestProjectSD.Models;

namespace TestProjectSD_withDatabase.Interface
{
    public interface IAuthRepository
    {
        public Auth? GetSingleAuthByName(string name);
        public void AddNewAuthData(UserForRegistrationDto userForRegistration);
    }
}
