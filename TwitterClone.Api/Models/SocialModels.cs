using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Api.Models;

public sealed record CreateUserRequest(
    [Required, StringLength(40, MinimumLength = 2)] string Username,
    [Required, EmailAddress] string Email);

public sealed record UpdatePhoneRequest([Required, Phone] string PhoneNumber);

public sealed record SendMessageRequest(
    [Required] Guid RecipientId,
    [Required, StringLength(2000, MinimumLength = 1)] string Text);

public sealed record SocialUser(Guid Id, string Username, string Email, string? PhoneNumber = null);

public sealed record MessageResponse(Guid Id, Guid SenderId, Guid RecipientId, string Text, bool IsRead, DateTime CreatedAt);
