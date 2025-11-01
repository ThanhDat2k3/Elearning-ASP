using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    public partial class Subject
    {
        public long SubjectId { get; set; }

        public string SubjectName { get; set; } = null!;

        public string? Description { get; set; }

        public    ICollection<Course> Courses { get; set; } = new List<Course>();

        public    ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}