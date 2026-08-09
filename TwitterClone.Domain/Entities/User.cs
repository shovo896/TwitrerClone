namespace TwitterClone.Domain.Entities;

public class User
{
    private readonly Guid _id;
    private string _username = string.Empty;
    private string _email = string.Empty;

    public User(string username, string email)
    {
        _id = Guid.NewGuid();
        Username = username;
        Email = email;
    }

    public Guid Id => _id;

    public string Username
    {
        get => _username;
        private set => _username = ValidateRequired(value, nameof(Username));
    }

    public string Email
    {
        get => _email;
        private set => _email = ValidateRequired(value, nameof(Email));
    }

    private static string ValidateRequired(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} is required.", fieldName);

        return value.Trim();
    }
}
