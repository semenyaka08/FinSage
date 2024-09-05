using Core.Domain.Models;
using Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comments.Include(z=>z.User).FirstOrDefaultAsync(z=>z.Id == id);
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.Include(z=>z.User).ToListAsync();
    }

    public async Task<Comment?> AddCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return await _context.Comments.Include(z=>z.User).FirstOrDefaultAsync(z=>z.Id == comment.Id);
    }

    public async Task<Comment?> UpdateCommentAsync(Comment updatedComment)
    {
        var comment = await GetCommentByIdAsync(updatedComment.Id);

        if (comment == null)
            return null;

        comment.Content = updatedComment.Content;
        comment.Title = updatedComment.Title;

        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        _context.Comments.Remove(comment);

        await _context.SaveChangesAsync();
    }
}