using Microsoft.AspNetCore.Mvc;
using TwitterClone.Api.Data;
using TwitterClone.Api.Dtos;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TweetsController(TweetRepository tweets, UserRepository users) : ControllerBase
{
    [HttpGet]
    public IActionResult GetTweets([FromQuery] Guid? userId) => Ok(tweets.GetTweets(userId));

    [HttpGet("{id:guid}")]
    public ActionResult<TweetDto> GetTweetById(Guid id) => tweets.GetTweetById(id) is { } tweet ? Ok(tweet) : NotFound();

    [HttpPost]
    public ActionResult<TweetDto> CreateTweet(CreateTweetRequest request)
    {
        if (users.GetUserById(request.AuthorId) is null) return BadRequest("Author does not exist.");
        try { var tweet = tweets.AddTweet(request); return CreatedAtAction(nameof(GetTweetById), new { id = tweet.Id }, tweet); }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
    }

    [HttpPut("{id:guid}")]
    public ActionResult<TweetDto> UpdateTweet(Guid id, UpdateTweetRequest request)
    {
        try { return tweets.UpdateTweet(id, request) is { } tweet ? Ok(tweet) : NotFound(); }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTweet(Guid id) => tweets.DeleteTweet(id) ? NoContent() : NotFound();
}
