using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.ValueObjects;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Student : AggregateRoot<StudentId>
{
    public string? Fullname { get; set; }
    public bool Gender { get; set; }
    public Address? Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public PlaceOfBirth? PlaceOfBirth { get; set; }
    public string? Occupation { get; set; }
    public List<SocialNetwork>? SocialNetwork { get; set; }

    public Student() : base(new StudentId(Guid.NewGuid()))
    { }

    public Student(
        string fullname,
        bool gender,
        Address? address,
        DateOnly dateOfBirth,
        PlaceOfBirth? placeOfBirth,
        string occupation,
        List<SocialNetwork>? socialNetwork) : base(new StudentId(Guid.NewGuid()))
        => (Fullname, Gender, Address, DateOfBirth, PlaceOfBirth, Occupation, SocialNetwork)
            = (fullname, gender, address, dateOfBirth, placeOfBirth, occupation, socialNetwork ?? new List<SocialNetwork>());

    public HashSet<ApplicationUser>? Users { get; private set; } = new();
    public HashSet<CourseEnrollment>? CourseEnrollments { get; private set; } = new();
}

public record StudentId(Guid Value);