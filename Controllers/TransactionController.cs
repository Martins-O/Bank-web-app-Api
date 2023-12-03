using BankAppWebApi.Data;
using BankAppWebApi.Dtos;
using BankAppWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankAppWebApi.Controllers
{
    [Route("api/v1/transaction/")]
    public class TransactionController : Controller
    {
        private readonly IAccountUserRepository _accountUserRepository;
        private readonly ApplicationDbContext _dbContext;

        public TransactionController(IAccountUserRepository accountUserRepository, ApplicationDbContext applicationDb)
        {
            _accountUserRepository = accountUserRepository;
            _dbContext = applicationDb;
        }

        [HttpPut("deposit")]
        public ResponseDto Deposit(decimal amount, string accNum)
        {
            var user = _accountUserRepository.GetAccountUserByAccountNumber(accNum);

            if (user != null)
            {
                user.Balance += amount;  

                _dbContext.SaveChanges(); 

                return ResponseDtoMethod(HttpStatusCode.OK, "Deposit successful", user);
            }

            return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
        }
        
        [HttpPut("withdraw")]
        public ResponseDto Withdraw(decimal amount, string accNum, string transactionPin)
        {
            var user = _accountUserRepository.GetAccountUserByAccountNumber(accNum);

            if (user.Balance < amount)
            {
                return ResponseDtoFailure(HttpStatusCode.BadRequest, "Insufficient funds in the source account!");
            }

            if(user.TransactionPin != transactionPin)
            {
                return ResponseDtoFailure(HttpStatusCode.BadRequest, "Wrong Pin!");
            }

            if (user != null)
            {
                user.Balance -= amount;  
                user.TransactionPin = transactionPin;

                _dbContext.SaveChanges(); 

                return ResponseDtoMethod(HttpStatusCode.OK, "Deposit successful", user);
            }

            return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
        }

        [HttpPost("transfer")]
        public ResponseDto Transfer(string sourceAccountNumber, string destinationAccountNumber, decimal amount, string transactionPin)
        {
            var sourceUser = _accountUserRepository.GetAccountUserByAccountNumber(sourceAccountNumber);
            var destinationUser = _accountUserRepository.GetAccountUserByAccountNumber(destinationAccountNumber);

            if (sourceUser == null || destinationUser == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "One or both accounts not found");
            }

            if (sourceUser.TransactionPin != transactionPin)
            {
                return ResponseDtoFailure(HttpStatusCode.BadRequest, "Wrong Pin!");
            }

            if (sourceUser.Balance < amount)
            {
                return ResponseDtoFailure(HttpStatusCode.BadRequest, "Insufficient funds in the source account");
            }

            sourceUser.Balance -= amount;
            destinationUser.Balance += amount;

            _dbContext.SaveChanges();

            return ResponseDtoMethod(HttpStatusCode.OK, $"Transfer of {amount} from {sourceAccountNumber} to {destinationAccountNumber} successful", destinationUser);
        }

        [HttpGet("balance/{accountNumber}")]
        public ResponseDto GetBalance(string accountNumber, string transactionPin)
        {
            var user = _accountUserRepository.GetAccountUserByAccountNumber(accountNumber);

            if (user.TransactionPin != transactionPin)
            {
                return ResponseDtoFailure(HttpStatusCode.BadRequest, "Wrong Pin!");
            }

            if (user == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
            }

            return ResponseDtoMethod(HttpStatusCode.OK, $"Balance of {accountNumber}: {user.Balance}", user);
        }

        [HttpPut("set-pin/{accountNumber}")]
        public ResponseDto SetTransactionPin(string accountNumber, string pin)
        {
            var user = _accountUserRepository.GetAccountUserByAccountNumber(accountNumber);

            if (user == null)
            {
                return ResponseDtoFailure(HttpStatusCode.NotFound, "User not found");
            }

            user.TransactionPin = pin;

            _dbContext.SaveChanges();

            return ResponseDtoMethod(HttpStatusCode.OK, $"Transaction PIN set for {accountNumber}", user);
        }

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

