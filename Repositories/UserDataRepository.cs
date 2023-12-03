using BankAppWebApi.Data;
using BankAppWebApi.Interfaces;
using BankAppWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAppWebApi.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {

        private readonly ApplicationDbContext _context;

        public UserDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateUserData(UserData userData)
        {
            _context.Add(userData);
            return Save();
        }

        public UserData AddUser(UserData user)
        {
            _context.UserDatas.Add(user);
            _context.SaveChanges();
            return user;
        }


        public UserData GetUserDataByEmailAddress(string Email)
        {
            return _context.UserDatas.Where(u => u.EmailAddress == Email).FirstOrDefault();
        }

        public UserData GetUserDataById(string Id)
        {
            return _context.UserDatas.Where(u => u.Id == Id).FirstOrDefault();
        }

        public ICollection<UserData> GetUserDatas()
        {
            return _context.UserDatas.OrderBy(u => u.Id).ToList();
        }

        public bool UserDataExist(string EmailAddress)
        {
            return _context.UserDatas.Any(u => u.EmailAddress == EmailAddress);
        }
    }
}
