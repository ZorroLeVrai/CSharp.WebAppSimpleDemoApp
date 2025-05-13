namespace WebAppDemo.Configuration;

/// <summary>
/// Configuration for JWT authentication.
/// </summary>
public class JwtConfig
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
