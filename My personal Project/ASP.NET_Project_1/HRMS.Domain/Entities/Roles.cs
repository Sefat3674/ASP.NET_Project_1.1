using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        // Navigation: One Role → Many Users
        public ICollection<Users> Users { get; set; }
    }
}