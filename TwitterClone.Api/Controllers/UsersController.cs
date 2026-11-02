using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Data;
using TwitterClone.Api.Dtos;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController(UserRepository users, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers() => Ok(users.GetUsers());

    [HttpPost]
    public ActionResult<UserDto> CreateUser(CreateUserRequest request)
    {
        if (users.GetUserByEmail(request.Email) is not null) return Conflict("A user with this email already exists.");
        var user = users.AddUser(request);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<UserDto> GetUserById(Guid id) => users.GetUserById(id) is { } user ? Ok(user) : NotFound();

    [HttpPut("{id:guid}")]
    public ActionResult<UserDto> UpdateUser(Guid id, UpdateUserRequest request)
    {
        var duplicate = users.GetUserByEmail(request.Email);
        if (duplicate is not null && duplicate.Id != id) return Conflict("A user with this email already exists.");
        return users.UpdateUser(id, request) is { } user ? Ok(user) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id) => users.DeleteUser(id) ? NoContent() : NotFound();

    [HttpGet("app-info")]
    public IActionResult GetAppInfo() => Ok(new { appName = configuration["AppName"], environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" });
}
