using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankAppWebApi.Models
{
    public class UserData : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
