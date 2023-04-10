using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(ApplicationDbContext context) : base(context) { }

    public void AddPaymentMethod(PaymentMethod paymentMethod) => Insert(paymentMethod);

    public void UpdatePaymentMethod(PaymentMethod paymentMethod) => Update(paymentMethod);

    public void DeletePaymentMethod(int? id) => Delete(x => x.paymentMethodId == id);

    public PaymentMethod? GetPaymentMethodById(int? id) => GetById(id);

    public IEnumerable<PaymentMethod> GetAllPaymentMethods() => GetAll();
}