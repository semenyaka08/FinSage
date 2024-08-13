using System.ComponentModel.DataAnnotations;

namespace Core.DTO;

public abstract class BaseCommentRequest
{
    [Required(ErrorMessage = "Title is required!")]
    [MinLength(5, ErrorMessage = "MinLength for Title is 5 characters!")]
    [MaxLength(30, ErrorMessage = "MaxLength for Title is 30 characters!")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required!")]
    [MinLength(5, ErrorMessage = "MinLength for Content is 5 characters!")]
    [MaxLength(180, ErrorMessage = "MaxLength for Content is 180 characters!")]
    public string Content { get; set; } = string.Empty;
}