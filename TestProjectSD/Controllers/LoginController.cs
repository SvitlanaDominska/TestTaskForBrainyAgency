
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProjectSD.Dtos;
using TestProjectSD.Helpers;
using TestProjectSD.Interface;
using TestProjectSD_withDatabase.Interface;

namespace WebApplicationDepIng.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class LoginController : ControllerBase
    {        
        private readonly IAuthRepository _authRepository;
        private readonly AuthHelper _authHelper;
        private readonly ICustomerRepository _customerRepository;
        public LoginController(IConfiguration config, IAuthRepository authRepository, ICustomerRepository customerRepository)
        {
            _authRepository = authRepository;
            _authHelper =new AuthHelper(config);
            _customerRepository = customerRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                var existingUser = _authRepository.GetSingleAuthByName(userForRegistration.Name);

                if (existingUser == null)
                {
                    try
                    {
                        _authRepository.AddNewAuthData(userForRegistration);

                        var customerExist = _customerRepository.GetSingleCustomers(userForRegistration.Name);

                        if (customerExist == null)
                        {
                            _customerRepository.AddCustomer(userForRegistration.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    return Ok();
                }
                return BadRequest($"User with name {userForRegistration.Name} already exists!");
            }
            return BadRequest("Password didn't match!");
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            const string _errorMessage = "The username or password is incorrect. Please try again or go to Registration.";

            var userForLoginConfirmation = _authRepository.GetSingleAuthByName(userForLogin.Name);

            if (userForLoginConfirmation == null)
            {
                return StatusCode(401, _errorMessage);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);

            
            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForLoginConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, _errorMessage);
                }
            }

            var customerNumber = _customerRepository.GetSingleCustomers(userForLogin.Name)?.CustomerNumber;
            if (customerNumber != null)
            {
                return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(customerNumber.Value)}
                    });
            }
            return BadRequest("Can't create token");

        }

       

    }
}
