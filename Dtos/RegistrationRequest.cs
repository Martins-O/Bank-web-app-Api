using System.ComponentModel.DataAnnotations;

namespace BankAppWebApi.Dtos
{
    public class RegistrationRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EmailAddress]
        public required string EmailAddress { get; set; }
        public required string Password { get; set; }
        public required string PhoneNum { get; set; }
        public int Age { get; set; }
    }
}
