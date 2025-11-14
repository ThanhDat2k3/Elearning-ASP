using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Subjects")]
    public partial class Subject
    {
        [Key]
        public long SubjectId { get; set; }

        [Required(ErrorMessage = "Tên môn học là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên môn học không được vượt quá 100 ký tự")]
        public string SubjectName { get; set; } = null!;

        public string? Description { get; set; }

        public    ICollection<Course> Courses { get; set; } = new List<Course>();

        public    ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}