using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Users")]    
    public    class User
    {
        public long UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? Phone { get; set; }

        public long RoleId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }

        public    Role Role { get; set; } = null!;

        public    ICollection<Student> Students { get; set; } = new List<Student>();

        public    ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}