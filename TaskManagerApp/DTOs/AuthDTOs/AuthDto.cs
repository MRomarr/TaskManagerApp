using System.Text.Json.Serialization;

namespace TaskManagerApp.DTOs.AuthDTOs
{
    public class AuthDto
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public List<string> Role { get; set; }
        [JsonIgnore]
        public bool IsAuthenticated { get; set; }
    }
}
