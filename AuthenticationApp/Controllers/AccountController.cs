using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // Pour la gestion des mots de passe
using System.Linq;
using System.Threading.Tasks;

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
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid email or password");
        }

        return Ok(new { message = "Login successful", userId = user.Id, email = user.Email });
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (_context.Users.Any(u => u.Email == model.Email))
        {
            return BadRequest("Email already in use");
        }

        var user = new User
        {
            Email = model.Email,
            Username = model.Username,
            PasswordHash = _passwordHasher.HashPassword(null, model.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Account created successfully", userId = user.Id, email = user.Email });
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterModel
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}