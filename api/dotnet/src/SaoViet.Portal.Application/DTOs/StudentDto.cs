namespace SaoViet.Portal.Application.DTOs;

public record StudentDto(
    string Id,
    string Fullname,
    bool Gender,
    string Address,
    string Dob,
    string Pod,
    string Occupation,
    string SocialNetwork);