using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Roles;
using WebApplication1.Models.UserProfile;

namespace WebApplication1.Models.Users
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        // Foreign Key to Role
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual WebApplication1.Models.Roles.Role Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // One User -> One UserProfile
        public virtual UserProfile.UserProfile UserProfile { get; set; }
    }
}