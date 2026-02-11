using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, StringLength(150)]
        public string FullName { get; set; }

        [Required, StringLength(150), EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}