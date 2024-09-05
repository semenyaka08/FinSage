using Core.Domain.RepositoryContracts;
using Core.DTO;
using Core.Mapper;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);

        if (comment == null)
            return NotFound();

        return Ok(comment.ToCommentGetResponse());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAllCommentsAsync();

        return Ok(comments.Select(z=>z.ToCommentGetResponse()));
    }

    [HasPermission(Permission.ReadMember)]
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> AddComment([FromRoute]int stockId, [FromBody] CommentAddRequest addRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userId = User.Claims.FirstOrDefault(z=>z.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if (!Guid.TryParse(userId, out Guid parsedId))
            return Unauthorized("Invalid user ID");
        
        var stock = await _stockRepository.GetByIdAsync(stockId);

        if (stock == null)
            return NotFound("Stock with given id was not found!");

        var comment = await _commentRepository.AddCommentAsync(addRequest.ToComment(stock, parsedId));

        return CreatedAtAction(nameof(GetCommentById), new {id = comment!.Id}, comment.ToCommentGetResponse());
    }

    [HasPermission(Permission.ReadMember)]
    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromQuery] int id, [FromBody] CommentUpdateRequest updateRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = updateRequest.ToComment(id);

        var result = await _commentRepository.UpdateCommentAsync(comment);

        if (result == null)
            return NotFound("comment with given id doe's not exist!!");

        return Ok(result.ToCommentGetResponse());
    }

    [HasPermission(Permission.ReadMember)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);

        if (comment == null)
            return NotFound("Comment with given id doe's not exist");

        await _commentRepository.DeleteCommentAsync(comment);

        return NoContent();
    }
}