using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LikesController(SocialStore store) : ControllerBase
{
    [HttpPost("{tweetId:guid}/like")]
    public IActionResult LikeTweet(Guid tweetId) { store.ToggleLike(SocialStore.DemoUserId, tweetId, out var liked); return Ok(new { tweetId, liked, count = store.LikeCount(tweetId) }); }
    [HttpDelete("{tweetId:guid}/unlike")]
    public IActionResult UnlikeTweet(Guid tweetId) { if (store.HasLiked(SocialStore.DemoUserId, tweetId)) store.ToggleLike(SocialStore.DemoUserId, tweetId, out _); return NoContent(); }
    [HttpGet("{tweetId:guid}/status/{userId:guid}")]
    public IActionResult GetLikeStatus(Guid tweetId, Guid userId) => Ok(new { tweetId, userId, liked = store.HasLiked(userId, tweetId) });
    [HttpGet("{tweetId:guid}/count")]
    public IActionResult GetLikeCount(Guid tweetId) => Ok(new { tweetId, count = store.LikeCount(tweetId) });
    [HttpGet("user/{userId:guid}")]
    public IActionResult GetLikedTweets(Guid userId) => Ok(new { userId, tweets = Array.Empty<Guid>() });
}
