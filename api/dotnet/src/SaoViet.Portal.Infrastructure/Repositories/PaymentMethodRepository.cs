using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class PaymentMethodRepository : RepositoryBase<PaymentMethod, PaymentMethodId>
{
    public PaymentMethodRepository(ApplicationDbContext context) : base(context)
    {
    }
}