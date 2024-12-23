using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // Pour la gestion des mots de passe
using System.Linq;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher; // Utilisation de IPasswordHasher pour hasher le mot de passe

    public AuthController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginModel model) // Le corps de la requête sera un objet JSON
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(new { message = "Login successful" });
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}