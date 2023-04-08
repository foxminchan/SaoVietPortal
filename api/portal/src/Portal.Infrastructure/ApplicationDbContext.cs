using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;

namespace Portal.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Student>? students { get; set; }
    public DbSet<Branch>? branches { get; set; }
    public DbSet<Course>? courses { get; set; }
    public DbSet<Class>? classes { get; set; }
    public DbSet<CourseEnrollment>? courseEnrollments { get; set; }
    public DbSet<Staff>? staffs { get; set; }
    public DbSet<Position>? positions { get; set; }
    public DbSet<ReceiptsExpenses>? receiptsExpenses { get; set; }
    public DbSet<StudentProgress>? studentProgresses { get; set; }
    public DbSet<CourseRegistration>? courseRegistrations { get; set; }
    public DbSet<PaymentMethod>? paymentMethods { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}