using BankAppWebApi.Dtos;
using BankAppWebApi.Interfaces;
using BankAppWebApi.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BankAppWebApi.Controllers
{
    public class UserController(IAccountUserRepository accountUserRepository, IUserDataRepository userDataRepository)
    {
        private readonly IAccountUserRepository _accountUserRepository = accountUserRepository;
        private readonly IUserDataRepository _userDataRepository = userDataRepository;

        [HttpPost("register")]
        public ResponseDto Register([FromBody] RegistrationRequest registerRequest)
        {
            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            if(!_userDataRepository.UserDataExist(registerRequest.EmailAddress))
            {
                var newUser = new UserData();
                newUser.EmailAddress = registerRequest.EmailAddress;
                newUser.FirstName = registerRequest.FirstName;
                newUser.LastName = registerRequest.LastName;
                newUser.PhoneNum = registerRequest.PhoneNum;
                newUser.Password = HashedPassword;
                newUser.CreatedAt = DateTime.Now;

                var savedUser = _userDataRepository.AddUser(newUser);

                var newAccUser = new AccountUser();
                newAccUser.AccountNunber = GenerateAccountNumber();
                newAccUser.UserDatas = savedUser;
                newAccUser.Balance = 0;
                newAccUser.CreatedAt = DateTime.Now;

                var savedAcc = _accountUserRepository.AddUser(newAccUser);
                return ResponseDtoMethod(HttpStatusCode.Created, "Register Succefully", savedAcc);
            }
            return ResponseDtoFailure(HttpStatusCode.BadRequest, "Register cannot be complete");
        }

        [HttpPost("login")]
        public ResponseDto Login([FromBody] LoginRequestDto loginRequestDto)
        {
            bool isValidCredentials = ValidateCredentials(loginRequestDto.emailAdress, loginRequestDto.password);

            if (isValidCredentials)
            {
                string token = GenerateToken(loginRequestDto.emailAdress);
                return ResponseDtoMethod(HttpStatusCode.Accepted, "Login succesfuly", token);
            }

            return ResponseDtoFailure(HttpStatusCode.BadGateway, "Login cannot be complated");
        }

        private string GenerateToken(string emailAddress)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHeregvisdbjkwebdasuibe833638789289"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, emailAddress),
            };

            var token = new JwtSecurityToken(
                issuer: "Bank App",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private bool ValidateCredentials(string emailAddress, string password)
        {
            UserData user = _userDataRepository.GetUserDataByEmailAddress(emailAddress);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true; 
            }

            return false; 
        }


        private ResponseDto ResponseDtoMethod(HttpStatusCode statusCode, string message, object data)
        {
            return new ResponseDto(statusCode, message, data);
        }
        private ResponseDto ResponseDtoFailure(HttpStatusCode statusCode, string message)
        {
            return new ResponseDto(statusCode, message);
        }

        private string GenerateAccountNumber()
        {
            string prefix = "90";
            string randomNumber = GenerateRandomNumber(100000, 999999).ToString();
            string accountNumber = $"{prefix}{randomNumber}";

            return accountNumber;
        }

        private int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
