using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Class")]
    public class Class
    {
        public long ClassId { get; set; }

        public string ClassName { get; set; } = null!;

        public int? Year { get; set; }

        public string? Description { get; set; }

        public    ICollection<Student> Students { get; set; } = new List<Student>();

        public    ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}