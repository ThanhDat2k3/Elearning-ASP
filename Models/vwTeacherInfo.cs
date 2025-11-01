using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Models
{
    [Table("vwTeacherInfo")]
    public class vwTeacherInfo
    {
        [Key]
        public long TeacherID { get; set; } 

        public long UserID { get; set; } 

        public string? TenGiangVien { get; set; } 

        public string? EmailHeThong { get; set; } 

        public string? EmailRieng { get; set; } 

        public string? SoDienThoai { get; set; }

        public string? HocVi { get; set; } 

        public string? NoiCongTac { get; set; } 

        public string? ChuyenMon { get; set; } 

        public string? GioiThieu { get; set; } 

        public string? AnhDaiDien { get; set; } 

        public string? Facebook { get; set; } 

        public string? Website { get; set; } 
        
        public DateTime? CreatedAt { get; set; } 

        public bool? IsActive { get; set; } 
    }
}