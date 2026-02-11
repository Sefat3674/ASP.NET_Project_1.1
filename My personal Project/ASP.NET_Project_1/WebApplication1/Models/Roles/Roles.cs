using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Users;

namespace WebApplication1.Models.Roles
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        // One Role -> Many Users
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}