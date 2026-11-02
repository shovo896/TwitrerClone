using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ReTweetsController(SocialStore store) : ControllerBase
{
    [HttpPost("{tweetId:guid}")]
    public IActionResult ReTweet(Guid tweetId) { store.ToggleRetweet(SocialStore.DemoUserId, tweetId, out var retweeted); return Ok(new { tweetId, retweeted }); }
    [HttpGet]
    public IActionResult GetReTweets() => Ok(new { message = "ReTweetsController is working!" });
    [HttpDelete("{tweetId:guid}")]
    public IActionResult DeleteReTweet(Guid tweetId) { if (store.HasRetweet(SocialStore.DemoUserId, tweetId)) store.ToggleRetweet(SocialStore.DemoUserId, tweetId, out _); return NoContent(); }
    [HttpGet("{tweetId:guid}/status")]
    public IActionResult GetReTweetStatus(Guid tweetId) => Ok(new { tweetId, retweeted = store.HasRetweet(SocialStore.DemoUserId, tweetId) });
}
