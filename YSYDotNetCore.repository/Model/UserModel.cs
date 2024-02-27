using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YSYDotNetCore.repository.Models
{
    [Table("Tbl_User")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserPassword { get; set; }
    }
}
