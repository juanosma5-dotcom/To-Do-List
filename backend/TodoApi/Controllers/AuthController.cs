using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly AppDbContext _db;
	private readonly AuthService _auth;
	public AuthController(AppDbContext db, AuthService auth) { _db = db; _auth = auth; }

	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginDto dto)
	{
		// Para demo: comparar texto (en prod usar hash y sal)
		var user = _db.Users.SingleOrDefault(u => u.Email == dto.Email && u.PasswordHash == dto.Password);
		if (user == null) return Unauthorized(new { message = "Credenciales inválidas" });

		var token = _auth.GenerateToken(user);
		return Ok(new { token });
	}
}
