namespace Portal.Domain.Entities
{
    public class CourseRegistration
    {
        public Guid courseRegistrationId { get; set; }
        public string? status { get; set; }
        public string? registerDate { get; set; }
        public string? appointmentDate { get; set; }
        public float registerFee { get; set; }
        public float discountAmount { get; set; }
        public int? paymentMethodId { get; set; }
        public PaymentMethod? paymentMethod { get; set; }
        public string? studentId { get; set; }
        public string? classId { get; set; }
        public CourseEnrollment? courseEnrollment { get; set; }
    }
}
