using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YSYDotNetCore.MvcApp.Models
{
    [Table("Tbl_UserLogin")]
    public class UserLoginDataModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? GeneratedToken { get; set; }
    }
}
