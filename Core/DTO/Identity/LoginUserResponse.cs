namespace Core.DTO.Identity;

public record LoginUserResponse(string Token, string UserName, string Email);