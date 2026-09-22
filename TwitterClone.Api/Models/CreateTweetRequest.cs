using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Api.Models;

public sealed record CreateTweetRequest(
    [Required, StringLength(280, MinimumLength = 1)] string Text,
    [Required, StringLength(50, MinimumLength = 2)] string Author,
    [Required, RegularExpression("^@[A-Za-z0-9_]{1,15}$")] string Handle);
