using Ardalis.SmartEnum;

namespace SaoViet.Portal.Domain.Enums;

public sealed class CertificateStatus : SmartEnum<CertificateStatus>
{
    public static readonly CertificateStatus Received = new(nameof(Received), 1);
    public static readonly CertificateStatus UnReceived = new(nameof(UnReceived), 2);
    public static readonly CertificateStatus NotReceived = new(nameof(NotReceived), 3);

    private CertificateStatus(string name, int value) : base(name, value)
    {
    }
}