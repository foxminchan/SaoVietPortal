using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    public void AddPaymentMethod(PaymentMethod paymentMethod);
    public void UpdatePaymentMethod(PaymentMethod paymentMethod);
    public void DeletePaymentMethod(int? id);
    public PaymentMethod? GetPaymentMethodById(int? id);
    public IEnumerable<PaymentMethod> GetAllPaymentMethods();
}