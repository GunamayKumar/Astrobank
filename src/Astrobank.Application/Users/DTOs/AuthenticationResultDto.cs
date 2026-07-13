namespace Astrobank.Application.Users.DTOs;

public class AuthenticationResultDto
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}
