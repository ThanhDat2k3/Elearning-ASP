using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Teachers")]
    public   class Teacher
    {
        [Key]
        public long TeacherId { get; set; }

        public long UserId { get; set; }

        public string? HocVi { get; set; }

        public string? NoiCongTac { get; set; }

        public string? GioiThieu { get; set; }

        public string? ChuyenMon { get; set; }

        public string? AnhDaiDien { get; set; }

        public string? Email { get; set; }

        public string? Facebook { get; set; }

        public string? Website { get; set; }

        public    ICollection<Course> Courses { get; set; } = new List<Course>();

        public    User User { get; set; } = null!;

        public    ICollection<Class> Classes { get; set; } = new List<Class>();

        public    ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
