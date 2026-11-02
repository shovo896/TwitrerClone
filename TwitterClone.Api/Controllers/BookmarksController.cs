using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BookmarksController(SocialStore store) : ControllerBase
{
    [HttpPost("{tweetId:guid}")]
    public IActionResult AddBookmark(Guid tweetId) => Toggle(tweetId);

    [HttpDelete("{tweetId:guid}")]
    public IActionResult RemoveBookmark(Guid tweetId)
    {
        if (store.HasBookmark(SocialStore.DemoUserId, tweetId)) store.ToggleBookmark(SocialStore.DemoUserId, tweetId, out _);
        return NoContent();
    }

    [HttpDelete]
    public IActionResult RemoveAllBookmarks() { store.RemoveBookmarks(SocialStore.DemoUserId); return NoContent(); }
    [HttpGet]
    public IActionResult GetBookmarks() => Ok(store.GetBookmarks(SocialStore.DemoUserId));
    [HttpGet("{tweetId:guid}/status")]
    public IActionResult GetBookmarkStatus(Guid tweetId) => Ok(new { tweetId, bookmarked = store.HasBookmark(SocialStore.DemoUserId, tweetId) });

    private IActionResult Toggle(Guid tweetId) { store.ToggleBookmark(SocialStore.DemoUserId, tweetId, out var bookmarked); return Ok(new { tweetId, bookmarked }); }
}
