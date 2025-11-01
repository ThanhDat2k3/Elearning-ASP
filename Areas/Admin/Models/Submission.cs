using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    public partial class Submission
    {
        public long SubmissionId { get; set; }

        public long? AssignmentId { get; set; }

        public long? StudentId { get; set; }

        public string? FilePath { get; set; }

        public DateTime? SubmitDate { get; set; }

        public double? Grade { get; set; }

        public    Assignment? Assignment { get; set; }

        public    Student? Student { get; set; }
    }
}