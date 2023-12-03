using BankAppWebApi.Data;
using BankAppWebApi.Dtos;
using BankAppWebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankAppWebApi.Controllers
{
    [Route("api/v1/user-details/")]
    [ApiController]
    public class UserDatailsController : Controller
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IAccountUserRepository _accountUserRepository;
       // private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public UserDatailsController(IUserDataRepository userDataRepository, IAccountUserRepository accountUserRepository, ApplicationDbContext dbContext)
        {
            _userDataRepository = userDataRepository;
            _accountUserRepository = accountUserRepository;
           // _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet("email/{emailAdress}")]
        public ResponseDto GetUserDataWithEmailAddress(string emailAddress)
        {
            var checkUser = _userDataRepository.GetUserDataByEmailAddress(emailAddress);

            if(checkUser == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
            }

            return ResponseDtoMethod(HttpStatusCode.OK, "User Details found", checkUser);
        }

        [HttpGet("id/{id}")]
        public ResponseDto GetUserDataWithId(string id)
        {
            var checkUser = _userDataRepository.GetUserDataById(id);

            if (checkUser == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
            }

            return ResponseDtoMethod(HttpStatusCode.OK, "User Details found", checkUser);
        }

        [HttpGet("accNo/{accountNumber}")]
        public ResponseDto GetAccUserWithAccount(string accountNumber)
        {
            var checkUser = _accountUserRepository.GetAccountUserByAccountNumber(accountNumber);

            if (checkUser == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
            }

            return ResponseDtoMethod(HttpStatusCode.OK, "User Details found", checkUser);
        }
      /// [HttpPatch("update/{emailAdress}")]
        //public IActionResult UpdateUser(string emailAdress, [FromBody] JsonPatchDocument<UserUpdateDto> patchDocument)
       // {
            //var user = _userDataRepository.GetUserDataByEmailAddress(emailAdress);

            //if (user == null)
            //{
              //  return NotFound("User not found");
            //}

            //var userToPatch = _mapper.Map<UserUpdateDto>(user);

            //patchDocument.ApplyTo(userToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            //if (!TryValidateModel(userToPatch))
            //{
              //  return ValidationProblem(ModelState);
            //}

            //_mapper.Map(userToPatch, user);
           // _dbContext.SaveChanges();

         //   return Ok("User updated successfully");
       // }
        private ResponseDto ResponseDtoMethod(HttpStatusCode statusCode, string message, object data)
        {
            return new ResponseDto(statusCode, message, data);
        }

        private ResponseDto ResponseDtoFailure(HttpStatusCode statusCode, string message)
        {
            return new ResponseDto(statusCode, message);
        }
    }
}
