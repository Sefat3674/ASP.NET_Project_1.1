using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

public class TestController : Controller
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (_context.Database.CanConnect())
        {
            return Content("Database connection successful!");
        }
        else
        {
            return Content("Cannot connect to database.");
        }
    }
}