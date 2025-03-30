using Api.TeamManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Database;

public partial class TeamManagementDbContext : DbContext
{
    public TeamManagementDbContext()
    {
    }

    public TeamManagementDbContext(DbContextOptions<TeamManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbDepartment> TbDepartments { get; set; }

    public virtual DbSet<TbDepartmentMember> TbDepartmentMembers { get; set; }

    public virtual DbSet<TbMember> TbMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=TeamManagementDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbDepartment>(entity =>
        {
            entity.ToTable("tbDepartment");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TbDepartmentMember>(entity =>
        {
            entity.ToTable("tbDepartmentMember");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Department).WithMany(p => p.TbDepartmentMembers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_tbDepartmentMember_tbDepartment");

            entity.HasOne(d => d.Member).WithMany(p => p.TbDepartmentMembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_tbDepartmentMember_tbMembers");
        });

        modelBuilder.Entity<TbMember>(entity =>
        {
            entity.ToTable("tbMembers");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(50);

            entity.HasOne(d => d.DepartmentMember).WithMany(p => p.TbMembers)
                .HasForeignKey(d => d.DepartmentMemberId)
                .HasConstraintName("FK_tbMembers_tbDepartmentMember");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}