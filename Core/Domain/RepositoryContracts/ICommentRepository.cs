using Core.Domain.Models;

namespace Core.Domain.RepositoryContracts;

public interface ICommentRepository
{
    public Task<Comment?> GetCommentByIdAsync(int id);

    public Task<List<Comment>> GetAllCommentsAsync();

    public Task AddCommentAsync(Comment comment);

    public Task<Comment?> UpdateCommentAsync(Comment updatedComment);

    public Task DeleteCommentAsync(Comment comment);
}