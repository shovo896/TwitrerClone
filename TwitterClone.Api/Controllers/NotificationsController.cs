using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class NotificationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNotifications() => Ok(Array.Empty<object>());
}
