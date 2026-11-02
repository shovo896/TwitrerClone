using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Models;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(SocialStore store, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers() => Ok(store.Users);

    [HttpPost]
    public ActionResult<SocialUser> CreateUser(CreateUserRequest request)
    {
        var user = store.AddUser(request.Username, request.Email);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<SocialUser> GetUserById(Guid id) => store.GetUser(id) is { } user ? Ok(user) : NotFound();

    [HttpPut("{id:guid}")]
    public ActionResult<SocialUser> UpdateUser(Guid id, CreateUserRequest request) =>
        store.UpdateUser(id, request.Username, request.Email) is { } user ? Ok(user) : NotFound();

    [HttpPatch("{id:guid}/phoneNumber")]
    public ActionResult<SocialUser> UpdatePhone(Guid id, UpdatePhoneRequest request) =>
        store.UpdatePhone(id, request.PhoneNumber) is { } user ? Ok(user) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id) => store.RemoveUser(id) ? NoContent() : NotFound();

    [HttpGet("app-info")]
    public IActionResult GetAppInfo() => Ok(new
    {
        appName = configuration["AppName"],
        environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
    });
}
