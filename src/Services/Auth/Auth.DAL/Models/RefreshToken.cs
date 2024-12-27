namespace Auth.DAL.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; }
}
