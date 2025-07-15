namespace TaskManagerApp.Helper
{
    public class JWTHelper
    {
        public const string Section = "JwtSettings";
        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int DurationInMinutes { get; set; }
    }
}
