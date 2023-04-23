namespace Portal.Domain.Entities;

public class PaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<CourseRegistration>? CourseRegistrations { get; private set; } = new HashSet<CourseRegistration>();
}