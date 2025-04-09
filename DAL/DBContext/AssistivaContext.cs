using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext;

public partial class AssistivaContext : DbContext
{
    public AssistivaContext()
    {
    }

    public AssistivaContext(DbContextOptions<AssistivaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<ClassroomStudent> ClassroomStudents { get; set; }

    public virtual DbSet<Disability> Disabilities { get; set; }

    public virtual DbSet<Formula> Formulas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentDisability> StudentDisabilities { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Classrooms_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classrooms_TeacherId");
        });

        modelBuilder.Entity<ClassroomStudent>(entity =>
        {
            entity.HasIndex(e => new { e.ClassroomId, e.StudentId }, "UQ_Classroom_Student").IsUnique();

            entity.HasOne(d => d.Classroom).WithMany(p => p.ClassroomStudents)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassroomStudents_ClassroomId");

            entity.HasOne(d => d.Student).WithMany(p => p.ClassroomStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassroomStudents_StudentId");
        });

        modelBuilder.Entity<Disability>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Disabilities_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Formula>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Subject).WithMany(p => p.Formulas)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_Subjects");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Roles_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.BloodType).HasMaxLength(15);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.EducationLevel).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaritalStatus).HasMaxLength(15);
            entity.Property(e => e.MaternalLastName).HasMaxLength(50);
            entity.Property(e => e.Neighborhood).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.PaternalLastName).HasMaxLength(50);
            entity.Property(e => e.PhotoUrl).HasMaxLength(200);
            entity.Property(e => e.Profession).HasMaxLength(50);
            entity.Property(e => e.ProfessionStatus).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(75);

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_UserId");
        });

        modelBuilder.Entity<StudentDisability>(entity =>
        {
            entity.HasIndex(e => new { e.StudentId, e.DisabilityId }, "UQ_Student_Disability").IsUnique();

            entity.HasOne(d => d.Disability).WithMany(p => p.StudentDisabilities)
                .HasForeignKey(d => d.DisabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDisabilities_DisabilityId");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentDisabilities)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDisabilities_StudentId");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasIndex(e => e.Code, "UQ_Subjetcs_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_Users_Username").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ExpirationCode).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPasswordDefect).HasDefaultValue(true);
            entity.Property(e => e.IsPasswordReset).HasDefaultValue(false);
            entity.Property(e => e.LastPasswordChange)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastPasswordReset).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.RecoveryCode).HasMaxLength(16);
            entity.Property(e => e.Salt).HasMaxLength(32);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UrlPicture).HasMaxLength(200);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
