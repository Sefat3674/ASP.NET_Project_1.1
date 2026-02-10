using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Users;

namespace WebApplication1.Models.UserProfile
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}