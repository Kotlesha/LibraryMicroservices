namespace Auth.DAL.Models;

public class Account(Guid id, string email, string hashPassword)
{
    public Guid Id { get; private set; } = id;
    public string Email { get; private set; } = email;
    public string HashPassword { get; private set; } = hashPassword;
}
