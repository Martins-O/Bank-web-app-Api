using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankAppWebApi.Models
{
    public class AccountUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string AccountNunber { get; set; } = string.Empty;
        [ForeignKey("UserDetailsId")]
        public virtual UserData UserDatas { get; set; }
        public decimal Balance{ get; set; }
        public DateTime CreatedAt { get; set; }
        public string TransactionPin { get; set; } = string.Empty;
    }
}
