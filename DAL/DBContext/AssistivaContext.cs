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

    public virtual DbSet<Disability> Disabilities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disability>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Disabilities_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
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
