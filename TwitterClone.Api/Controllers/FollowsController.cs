using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FollowsController(SocialStore store) : ControllerBase
{
    [HttpPost("{userId:guid}")]
    public IActionResult Follow(Guid userId) { store.ToggleFollow(SocialStore.DemoUserId, userId, out var following); return Ok(new { userId, following }); }
    [HttpDelete("{userId:guid}")]
    public IActionResult Unfollow(Guid userId) { if (store.IsFollowing(SocialStore.DemoUserId, userId)) store.ToggleFollow(SocialStore.DemoUserId, userId, out _); return NoContent(); }
    [HttpGet("{userId:guid}/status")]
    public IActionResult GetStatus(Guid userId) => Ok(new { userId, following = store.IsFollowing(SocialStore.DemoUserId, userId) });
}
