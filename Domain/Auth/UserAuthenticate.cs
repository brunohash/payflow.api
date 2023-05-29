namespace Domain.Auth;

public class UserAuthenticate
{
    public int Id { get; set; }
    public string? User { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
}