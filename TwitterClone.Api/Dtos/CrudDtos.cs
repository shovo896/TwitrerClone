using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Api.Dtos;

public sealed record CreateUserRequest([Required, StringLength(40, MinimumLength = 2)] string Username, [Required, EmailAddress] string Email);
public sealed record UpdateUserRequest([Required, StringLength(40, MinimumLength = 2)] string Username, [Required, EmailAddress] string Email);
public sealed record UserDto(Guid Id, string Username, string Email, string? PhoneNumber = null);
public sealed record CreateTweetRequest([Required] Guid AuthorId, [Required, StringLength(280, MinimumLength = 1)] string Content);
public sealed record UpdateTweetRequest([Required, StringLength(280, MinimumLength = 1)] string Content);
public sealed record TweetDto(Guid Id, Guid AuthorId, string Content, DateTime CreatedAt);
