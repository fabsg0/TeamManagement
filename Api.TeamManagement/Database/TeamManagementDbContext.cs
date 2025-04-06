using Api.TeamManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Database;

public partial class TeamManagementDbContext(DbContextOptions<TeamManagementDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Data Source=BrunnerNetwork-SV01\\fabsg0SV;Initial Catalog=TeamManagementDb;User=sa;Password=Leisach150!;TrustServerCertificate=True");
    
    public virtual DbSet<TbDepartment> TbDepartments { get; set; }

    public virtual DbSet<TbDepartmentMember> TbDepartmentMembers { get; set; }

    public virtual DbSet<TbMember> TbMembers { get; set; }

    public virtual DbSet<TbMembershipFeePayment> TbMembershipFeePayments { get; set; }

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
        });

        modelBuilder.Entity<TbMembershipFeePayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbMemberId");

            entity.ToTable("tbMembershipFeePayments");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PaymentPeriod).HasDefaultValueSql("(datepart(year,getdate()))");

            entity.HasOne(d => d.Member).WithMany(p => p.TbMembershipFeePayments)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_tbMembershipFeePayments_tbMembers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
