using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeReview.Models;

public partial class ReviewSystemContext : DbContext
{
    public ReviewSystemContext()
    {
    }

    public ReviewSystemContext(DbContextOptions<ReviewSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CantactInfo> CantactInfos { get; set; }

    public virtual DbSet<DocumentInfo> DocumentInfos { get; set; }

    public virtual DbSet<Review> EmployeeSkills { get; set; }

    public virtual DbSet<EmployeeUser> EmployeeUsers { get; set; }

    public virtual DbSet<EmpReview> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Category> Categoryies { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<GetAllUserWithRole> GetAllUserWithRoles { get; set; }

    public virtual DbSet<EditUserRole> EditUserRoles { get; set; }
    public virtual DbSet<EmployeeReviewModel> EmployeeReviewModels { get; set; }

    public virtual DbSet<getReviewWithCategory> GetReviewWithCategories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-R3345JT;Database=ReviewSystem;Trusted_Connection=True; User ID=sa; Password=test; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CantactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__CantactI__5C66259BD44F6F65");

            entity.ToTable("CantactInfo");

            entity.Property(e => e.ContactId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AlternativeEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContactRelation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contactRelation");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Facebook)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkedIn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentCountry)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentState)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Skype)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryCountry)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryState)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");

            entity.HasOne(d => d.User).WithMany(p => p.CantactInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserInfoContactInfo_Fk");
        });

        modelBuilder.Entity<DocumentInfo>(entity =>
        {
            entity.HasKey(e => e.DocumnetId).HasName("PK__Document__F958D873115ED2E0");

            entity.ToTable("DocumentInfo");

            entity.Property(e => e.DocumnetId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AcademicQualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CitizenshipIssuePlace)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CitizenshipNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Citnumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CITNumber");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.DocName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PanNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pfnumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PFNumber");
            entity.Property(e => e.SelectDocType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sffnumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SFFNumber");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");


        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ReviewBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReviewDate).HasColumnType("date");
            entity.Property(e => e.SkillRating)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");
        });

        modelBuilder.Entity<EmployeeUser>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11E00EEF2D");

            entity.ToTable("EmployeeUser");

            entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateOfAppointment).HasColumnType("date");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserEmployee_Fk");
        });

        modelBuilder.Entity<EmpReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79CEC00FB1DB");

            entity.ToTable("Review");

            entity.Property(e => e.ReviewId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A73B83FAE");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Skill__DFA091874322E983");

            entity.ToTable("Skill");

            entity.Property(e => e.CategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryName");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserInfo__8D663599FB146028");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Religion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");

        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRole");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("date");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_Table");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersRole_Table");
        });
        modelBuilder.Entity<GetAllUserWithRole>().HasNoKey();
        modelBuilder.Entity<EditUserRole>().HasNoKey();
        modelBuilder.Entity<EmployeeReviewModel>().HasNoKey();
        modelBuilder.Entity<getReviewWithCategory>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
