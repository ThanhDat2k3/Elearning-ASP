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

        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(150, ErrorMessage = "Họ và tên không được vượt quá 150 ký tự")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email đăng nhập là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(200, ErrorMessage = "Email không được vượt quá 200 ký tự")]
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