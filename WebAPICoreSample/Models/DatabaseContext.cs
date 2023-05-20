using Microsoft.EntityFrameworkCore;

namespace WebAPICoreSample.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee>? Employees { get; set; }
        public virtual DbSet<UserInfo>? UserInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("UserInfo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CreatedDate).IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
                entity.Property(e => e.NationalIDNumber).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.EmployeeName).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.LoginID).HasMaxLength(256).IsUnicode(false);
                entity.Property(e => e.JobTitle).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.BirthDate).IsUnicode(false);
                entity.Property(e => e.MaritalStatus).HasMaxLength(1).IsUnicode(false);
                entity.Property(e => e.Gender).HasMaxLength(1).IsUnicode(false);
                entity.Property(e => e.HireDate).IsUnicode(false);
                entity.Property(e => e.VacationHours).IsUnicode(false);
                entity.Property(e => e.SickLeaveHours).IsUnicode(false);
                entity.Property(e => e.RowGuid).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ModifiedDate).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
