using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TwitterClone.Api.Models;
using TwitterClone.Api.Options;
using TwitterClone.Api.Services;

namespace TwitterClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TweetsController(TweetStore store, IOptions<TwitterSettings> settings) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<TweetResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetTweets() => Ok(new
    {
        maxTweetLength = settings.Value.MaxTweetLength,
        tweets = store.GetAll()
    });

    [HttpGet("{id:int}")]
    [ProducesResponseType<TweetResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TweetResponse> GetTweet(int id)
    {
        var tweet = store.GetById(id);
        return tweet is null ? NotFound() : Ok(tweet);
    }

    [HttpPost]
    [ProducesResponseType<TweetResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TweetResponse> CreateTweet(CreateTweetRequest request)
    {
        if (request.Text.Trim().Length > settings.Value.MaxTweetLength)
        {
            ModelState.AddModelError(nameof(request.Text), $"Tweet cannot exceed {settings.Value.MaxTweetLength} characters.");
            return ValidationProblem(ModelState);
        }

        var tweet = store.Create(request);
        return CreatedAtAction(nameof(GetTweet), new { id = tweet.Id }, tweet);
    }
}
