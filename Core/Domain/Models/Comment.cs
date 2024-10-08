namespace Core.Domain.Models;

public class Comment
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public int StockId { get; set; }

    public Stock Stock { get; set; } = null!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}