using BankAppWebApi.Data;
using BankAppWebApi.Interfaces;
using BankAppWebApi.Models;
using System.Net.Mail;

namespace BankAppWebApi.Repositories
{
    public class AccountUserRepository : IAccountUserRepository
    {

        private readonly ApplicationDbContext _context;

        public AccountUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AccountUserExist(string AccountNumber)
        {
            return _context.AccountUsers.Any(a => a.AccountNunber == AccountNumber);
        }

        public AccountUser AddUser(AccountUser user)
        {
            _context.AccountUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        public AccountUser GetAccountUserByAccountNumber(string AccountNumber)
        {
            return _context.AccountUsers.Where(a => a.AccountNunber == AccountNumber).FirstOrDefault();
        }

        public AccountUser GetAccountUserById(string Id)
        {
            return _context.AccountUsers.Where(a => a.Id == Id).FirstOrDefault();
        }

        public ICollection<AccountUser> GetAccountUsers()
        {
            return _context.AccountUsers.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateAccountUser(AccountUser accountUser)
        {
            _context.Add(accountUser);
            return Save();
        }
    }
}
