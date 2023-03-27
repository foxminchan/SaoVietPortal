namespace Portal.Domain.Entities
{
    public class PaymentMethod
    {
        public int paymentMethodId { get; set; }
        public string? paymentMethodName { get; set; }
        public List<CourseRegistration>? courseRegistrations { get; set; }
    }
}
