namespace Portal.Api.Models;

public record Student(
    string Id,
    string Fullname,
    bool Gender,
    string Address,
    string Dob,
    string Pod,
    string Occupation,
    string SocialNetwork);