using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly SqliteConnection _connection;

    public AuthController(SqliteConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        await _connection.OpenAsync();
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, PasswordHash FROM Users WHERE Email = @Email";
        command.Parameters.AddWithValue("@Email", model.Email);

        using (var reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                var userId = reader.GetInt32(0);
                var storedPasswordHash = reader.GetString(1);

                if (VerifyPasswordHash(model.Password, storedPasswordHash))
                {
                    return Ok(new { message = "Login successful", userId, email = model.Email });
                }
            }
        }

        return Unauthorized("Invalid email or password");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        await _connection.OpenAsync();
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
        command.Parameters.AddWithValue("@Email", model.Email);

        var count = (long)await command.ExecuteScalarAsync();
        if (count > 0)
        {
            return BadRequest("Email already in use");
        }

        var passwordHash = HashPassword(model.Password);

        command.CommandText = "INSERT INTO Users (Email, Username, PasswordHash) VALUES (@Email, @Username, @PasswordHash)";
        command.Parameters.AddWithValue("@Username", model.Username);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);

        await command.ExecuteNonQueryAsync();

        return Ok(new { message = "Account created successfully", email = model.Email });
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        var hash = HashPassword(password);
        return hash == storedHash;
    }
}

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterModel
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}