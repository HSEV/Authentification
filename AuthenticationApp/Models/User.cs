using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Username { get; set; } = string.Empty;
    public new string Email { get; set; } = string.Empty;
    public new string PasswordHash { get; set; } = string.Empty;
}