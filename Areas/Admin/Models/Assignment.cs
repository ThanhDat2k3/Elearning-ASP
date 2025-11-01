using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Assignment")]
public class Assignment
{
    public long AssignmentId { get; set; }

    public long? CourseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public    Course? Course { get; set; }

    public    ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
}