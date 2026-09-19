using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Api.Options;

public sealed class TwitterSettings
{
    public const string SectionName = "TwitterSettings";

    [Range(1, 1000)]
    public int MaxTweetLength { get; init; } = 280;
}
