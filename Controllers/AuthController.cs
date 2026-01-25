using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    public AuthController(PortfolioDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Email == request.Email);

        if (admin == null)
            return Unauthorized("Invalid credentials");

        bool isValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            admin.PasswordHash
        );

        if (!isValid)
            return Unauthorized("Invalid credentials");

        return Ok("Login successful");
    }
}
