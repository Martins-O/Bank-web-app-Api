using System.ComponentModel.DataAnnotations;

namespace BankAppWebApi.Dtos
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string emailAdress { get; set; }
        public string password { get; set; }
    }
}
