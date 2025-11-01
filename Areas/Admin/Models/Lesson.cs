using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    public partial class Lesson
    {
        public long LessonId { get; set; }

        public long? CourseId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? VideoLink { get; set; }

        public DateTime? UploadDate { get; set; }

        public    Course? Course { get; set; }
    }
}