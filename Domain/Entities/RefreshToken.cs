namespace Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string? RefreshTokenValue { get; set; }
    public DateTime ExpireTime { get; set; }
}
