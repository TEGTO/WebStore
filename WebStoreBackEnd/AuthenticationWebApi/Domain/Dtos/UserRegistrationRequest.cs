namespace AuthenticationWebApi.Domain.Dtos
{
    public class UserRegistrationRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
