using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers() => Ok(new[]
    {
        new { id = 1, name = "Shuvo", handle = "@shuvo" },
        new { id = 2, name = "CPS Academy", handle = "@cpsacademy" },
        new { id = 3, name = "ASP.NET Learner", handle = "@dotnetlearner" }
    });

    [HttpGet("app-info")]
    public IActionResult GetAppInfo() => Ok(new
    {
        appName = configuration["AppName"],
        environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
    });
}
