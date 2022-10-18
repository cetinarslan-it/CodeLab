
using api.Extensions;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Insurance> Insurances => Set<Insurance>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Insurance>(entity =>
        {
            entity.HasKey(x => x.InsuranceId);
            entity.Property(x=> x.Name);
            entity.Property(x=> x.Value);
            entity.HasMany(e => e.Children)
                .WithOne(e => e.Parent) 
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false);
        });
    }
}