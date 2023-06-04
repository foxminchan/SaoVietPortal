using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDatabaseFacade, IDomainEventContext
{
    public DbSet<Branch>? Branches { get; set; }
    public DbSet<Certificate>? Certificates { get; set; }
    public DbSet<Class>? Classes { get; set; }
    public DbSet<Course>? Courses { get; set; }
    public DbSet<CourseEnrollment>? CourseEnrollments { get; set; }
    public DbSet<CourseRegistration>? CourseRegistrations { get; set; }
    public DbSet<CourseType>? CourseTypes { get; set; }
    public DbSet<Curriculum>? Curricula { get; set; }
    public DbSet<PaymentMethod>? PaymentMethods { get; set; }
    public DbSet<Position>? Positions { get; set; }
    public DbSet<ReceiptsExpenses>? ReceiptsExpenses { get; set; }
    public DbSet<Staff>? Staffs { get; set; }
    public DbSet<Student>? Students { get; set; }
    public DbSet<StudentProgress>? StudentProgresses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public IEnumerable<IDomainEvent> GetDomainEvents<T>() where T : notnull
    {
        var domainEntities = ChangeTracker
            .Entries<RootBaseEntity<T>>()
            .Where(x =>
                x.Entity.DomainEvents.Any())
            .ToImmutableList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToImmutableList();

        domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

        return domainEvents;
    }
}