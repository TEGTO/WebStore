namespace WebStoreBackEnd.Models.Dto
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
