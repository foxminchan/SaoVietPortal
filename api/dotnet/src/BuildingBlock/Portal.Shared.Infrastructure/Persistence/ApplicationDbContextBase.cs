using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portal.Shared.Infrastructure.Persistence;

public abstract class ApplicationDbContextBase : IdentityDbContext, IDatabaseFacade
{
    protected abstract string Schema { get; }

    protected ApplicationDbContextBase(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
            builder.HasDefaultSchema(Schema);

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContextBase).Assembly);
    }
}