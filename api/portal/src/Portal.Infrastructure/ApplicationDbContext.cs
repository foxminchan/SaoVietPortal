using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;

namespace Portal.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Student>? Students { get; set; }
    public DbSet<Branch>? Branches { get; set; }
    public DbSet<Course>? Courses { get; set; }
    public DbSet<Class>? Classes { get; set; }
    public DbSet<CourseEnrollment>? CourseEnrollments { get; set; }
    public DbSet<Staff>? Staffs { get; set; }
    public DbSet<Position>? Positions { get; set; }
    public DbSet<ReceiptsExpenses>? ReceiptsExpenses { get; set; }
    public DbSet<StudentProgress>? StudentProgresses { get; set; }
    public DbSet<CourseRegistration>? CourseRegistrations { get; set; }
    public DbSet<PaymentMethod>? PaymentMethods { get; set; }

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