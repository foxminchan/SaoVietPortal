using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    public void AddPaymentMethod(PaymentMethod paymentMethod);

    public void UpdatePaymentMethod(PaymentMethod paymentMethod);

    public void DeletePaymentMethod(int? id);

    public bool TryGetPaymentMethod(int? id, out PaymentMethod? paymentMethod);

    public IEnumerable<PaymentMethod> GetAllPaymentMethods();
}