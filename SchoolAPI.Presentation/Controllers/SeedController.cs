using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data.Context;
using SchoolAPI.Data.Seeding;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public SeedController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedDatabase()
    {
        await DatabaseSeeder.SeedAsync(_context);
        return Ok("Database seeded successfully");
    }
}