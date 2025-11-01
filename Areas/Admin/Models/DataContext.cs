using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Elearning.Areas.Admin.Models
{
    
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public   DbSet<AdminMenu> AdminMenus { get; set; }

        public   DbSet<Assignment> Assignments { get; set; }

        public   DbSet<Class> Classes { get; set; }

        public   DbSet<Course> Courses { get; set; }

        public   DbSet<Enrollment> Enrollments { get; set; }

        public   DbSet<Lesson> Lessons { get; set; }

        public   DbSet<Role> Roles { get; set; }

        public   DbSet<Student> Students { get; set; }

        public   DbSet<Subject> Subjects { get; set; }

        public   DbSet<Submission> Submissions { get; set; }

        public   DbSet<tblMenu> TblMenus { get; set; }

        public   DbSet<Teacher> Teachers { get; set; }

        public   DbSet<User> Users { get; set; }

        public   DbSet<vwTeacherInfo> vwTeacherInfos { get; set; }

    /*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=Elearning;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminMenu>(entity =>
            {
                entity.ToTable("AdminMenu");

                entity.Property(e => e.AdminMenuId).HasColumnName("AdminMenuID");
                entity.Property(e => e.ActionName).HasMaxLength(50);
                entity.Property(e => e.AreaName).HasMaxLength(50);
                entity.Property(e => e.ControllerName).HasMaxLength(50);
                entity.Property(e => e.Icon).HasMaxLength(50);
                entity.Property(e => e.IdName).HasMaxLength(50);
                entity.Property(e => e.ItemName).HasMaxLength(50);
                entity.Property(e => e.ItemTarget).HasMaxLength(50);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E57B583DFEB");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
                entity.Property(e => e.CourseId).HasColumnName("CourseID");
                entity.Property(e => e.Deadline).HasColumnType("datetime");
                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Assignmen__Cours__71D1E811");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A0AAFC5B9D");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");
                entity.Property(e => e.ClassName).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71870B59782D");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");
                entity.Property(e => e.CourseName).HasMaxLength(100);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValue("Ðang m?");
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.HasOne(d => d.Subject).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Courses__Subject__5FB337D6");

                entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Courses__Teacher__60A75C0F");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FB264D475E");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
                entity.Property(e => e.CourseId).HasColumnName("CourseID");
                entity.Property(e => e.EnrolledDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValue("Ðang h?c");
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Enrollmen__Cours__693CA210");

                entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Enrollmen__Stude__68487DD7");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACB0C2C2D332");

                entity.Property(e => e.LessonId).HasColumnName("LessonID");
                entity.Property(e => e.CourseId).HasColumnName("CourseID");
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UploadDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.VideoLink).HasMaxLength(255);

                entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Lessons__CourseI__6E01572D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A83B2C0A4");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A793E6C43A2");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");
                entity.Property(e => e.ClassId).HasColumnName("ClassID");
                entity.Property(e => e.StudentCode).HasMaxLength(20);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Class).WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Students__ClassI__656C112C");

                entity.HasOne(d => d.User).WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Students__UserID__6477ECF3");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388CAC0210E");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
                entity.Property(e => e.SubjectName).HasMaxLength(100);
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE10519E4183E");

                entity.Property(e => e.SubmissionId).HasColumnName("SubmissionID");
                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
                entity.Property(e => e.FilePath).HasMaxLength(255);
                entity.Property(e => e.StudentId).HasColumnName("StudentID");
                entity.Property(e => e.SubmitDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Assignment).WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK__Submissio__Assig__74AE54BC");

                entity.HasOne(d => d.Student).WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Submissio__Stude__75A278F5");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("tblMenu");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");
                entity.Property(e => e.ActionName).HasMaxLength(50);
                entity.Property(e => e.ControllerName).HasMaxLength(50);
                entity.Property(e => e.Link).HasMaxLength(50);
                entity.Property(e => e.MenuName).HasMaxLength(50);
                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF259449C4DEDB6");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
                entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
                entity.Property(e => e.ChuyenMon).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Facebook).HasMaxLength(255);
                entity.Property(e => e.HocVi).HasMaxLength(50);
                entity.Property(e => e.NoiCongTac).HasMaxLength(200);
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Website).HasMaxLength(255);

                entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teachers__UserID__5535A963");

                entity.HasMany(d => d.Classes).WithMany(p => p.Teachers)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeacherClass",
                        r => r.HasOne<Class>().WithMany()
                            .HasForeignKey("ClassId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__TeacherCl__Class__5CD6CB2B"),
                        l => l.HasOne<Teacher>().WithMany()
                            .HasForeignKey("TeacherId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__TeacherCl__Teach__5BE2A6F2"),
                        j =>
                        {
                            j.HasKey("TeacherId", "ClassId").HasName("PK__TeacherC__F143CB3E03B81BC3");
                            j.ToTable("TeacherClasses");
                            j.IndexerProperty<long>("TeacherId").HasColumnName("TeacherID");
                            j.IndexerProperty<long>("ClassId").HasColumnName("ClassID");
                        });

                entity.HasMany(d => d.Subjects).WithMany(p => p.Teachers)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeacherSubject",
                        r => r.HasOne<Subject>().WithMany()
                            .HasForeignKey("SubjectId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__TeacherSu__Subje__59063A47"),
                        l => l.HasOne<Teacher>().WithMany()
                            .HasForeignKey("TeacherId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__TeacherSu__Teach__5812160E"),
                        j =>
                        {
                            j.HasKey("TeacherId", "SubjectId").HasName("PK__TeacherS__7733E37C64EFCE73");
                            j.ToTable("TeacherSubjects");
                            j.IndexerProperty<long>("TeacherId").HasColumnName("TeacherID");
                            j.IndexerProperty<long>("SubjectId").HasColumnName("SubjectID");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC4E821EF0");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105346D08C500").IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.PasswordHash).HasMaxLength(200);
                entity.Property(e => e.Phone).HasMaxLength(15);
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleID__4CA06362");
            });

            modelBuilder.Entity<VwTeacherInfo>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vwTeacherInfo");

                entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
                entity.Property(e => e.ChuyenMon).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.EmailHeThong).HasMaxLength(100);
                entity.Property(e => e.EmailRieng).HasMaxLength(100);
                entity.Property(e => e.Facebook).HasMaxLength(255);
                entity.Property(e => e.HocVi).HasMaxLength(50);
                entity.Property(e => e.NoiCongTac).HasMaxLength(200);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
                entity.Property(e => e.TenGiangVien).HasMaxLength(100);
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Website).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
    }
}
