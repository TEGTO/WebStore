namespace AuthenticationWebApi.Models.Dto
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
