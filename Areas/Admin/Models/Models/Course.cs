using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Course")]
    public class Course
    {
        public long CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public string? Description { get; set; }

        public long? SubjectId { get; set; }

        public long? TeacherId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Status { get; set; }

        public    ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        public    ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public    ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public    Subject? Subject { get; set; }

        public    Teacher? Teacher { get; set; }
    }
}