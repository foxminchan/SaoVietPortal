using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class PaymentMethodService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentMethodService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddPaymentMethod(PaymentMethod paymentMethod) 
        => _unitOfWork.paymentMethodRepository.AddPaymentMethod(paymentMethod);

    public void UpdatePaymentMethod(PaymentMethod paymentMethod) 
        => _unitOfWork.paymentMethodRepository.UpdatePaymentMethod(paymentMethod);

    public void DeletePaymentMethod(int id) => _unitOfWork.paymentMethodRepository.DeletePaymentMethod(id);

    public bool TryGetPaymentMethod(int id, out PaymentMethod? paymentMethod) 
        => _unitOfWork.paymentMethodRepository.TryGetPaymentMethod(id, out paymentMethod);

    public IEnumerable<PaymentMethod> GetAllPaymentMethods() => _unitOfWork.paymentMethodRepository.GetAllPaymentMethods();
}