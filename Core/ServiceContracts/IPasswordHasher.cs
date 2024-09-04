namespace Core.ServiceContracts;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool Verify(string password, string hashedPassword);
}