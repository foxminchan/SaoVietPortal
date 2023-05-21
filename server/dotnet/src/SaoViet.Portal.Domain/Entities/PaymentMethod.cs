using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class PaymentMethod : AggregateRoot<PaymentMethodId>
{
    public string? Name { get; set; }

    public PaymentMethod() : base(new PaymentMethodId(0))
    { }

    public PaymentMethod(PaymentMethodId id, string name) : base(id)
        => Name = name;

    public HashSet<CourseRegistration> CourseRegistrations { get; private set; } = new();
}

public record PaymentMethodId(int Value);