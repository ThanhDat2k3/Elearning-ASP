using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    public partial class Student
    {
        public long StudentId { get; set; }

        public long UserId { get; set; }

        public long? ClassId { get; set; }

        public string? StudentCode { get; set; }

        public DateOnly? BirthDate { get; set; }

        public    Class? Class { get; set; }

        public    ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public    ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        public    User User { get; set; } = null!;
    }
}