using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    public partial class Enrollment
    {
        public long EnrollmentId { get; set; }

        public long? StudentId { get; set; }

        public long? CourseId { get; set; }

        public DateTime? EnrolledDate { get; set; }

        public string? Status { get; set; }

        public    Course? Course { get; set; }

        public    Student? Student { get; set; }
    }
}
