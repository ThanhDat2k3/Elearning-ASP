using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    [Table("Roles")]
    public  class Role
    {
        [Key]
        public long RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public    ICollection<User> Users { get; set; } = new List<User>();
    }
}
