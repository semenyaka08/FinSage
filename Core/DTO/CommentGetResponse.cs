namespace Core.DTO;

public record CommentGetResponse(int Id, string Title, string Content, Guid UserId ,string UserName);