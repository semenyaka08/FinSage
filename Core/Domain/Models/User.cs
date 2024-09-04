namespace Core.Domain.Models;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Stock> Stocks { get; set; } = [];

    public ICollection<Comment> Comments { get; set; } = [];
}