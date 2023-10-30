namespace BuberDinner.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; set; }
    public string issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}