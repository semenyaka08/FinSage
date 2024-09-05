using Core.Domain.Models;
using Core.DTO;

namespace Core.Mapper;

public static class CommentMapper
{
    public static CommentGetResponse ToCommentGetResponse(this Comment comment)
    {
        return new CommentGetResponse(comment.Id, comment.Title,comment.Content, comment.UserId, comment.User.UserName);
    }

    public static Comment ToComment(this BaseCommentRequest request, Stock stock, Guid userId)
    {
        return new Comment
        {
            UserId = userId,
            Title = request.Title,
            Content = request.Content,
            CreatedOn = DateTime.Now,
            Stock = stock
        };
    }

    public static Comment ToComment(this BaseCommentRequest request, int id)
    {
        return new Comment
        {
            Id = id,
            Title = request.Title,
            Content = request.Content,
        };
    }
}