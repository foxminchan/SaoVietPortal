using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.Enums;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Certificate : AggregateRoot<CertificateId>
{
    public string? Name { get; set; }
    public DateOnly CertificateDate { get; set; }
    public CertificateStatus? Status { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public DateOnly? ReceivedDate { get; set; }
    public byte Rating { get; set; }
    public CourseEnrollmentId? CourseEnrollmentId { get; set; }

    public Certificate() : base(new CertificateId(Guid.NewGuid()))
    { }

    public Certificate(
        string name,
        DateOnly certificateDate,
        CertificateStatus status,
        DateOnly? expiryDate,
        DateOnly? receivedDate,
        byte rating,
        CourseEnrollmentId courseEnrollmentId)
        : base(new CertificateId(Guid.NewGuid()))
        => (Name, CertificateDate, Status, ExpiryDate, ReceivedDate, Rating, CourseEnrollmentId)
            = (name, certificateDate, status, expiryDate, receivedDate, rating, courseEnrollmentId);

    public CourseEnrollment? CourseEnrollment { get; set; }
}

public record CertificateId(Guid Value);