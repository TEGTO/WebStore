namespace WebStoreBackEnd.Models.Dto
{
    public class UserRegistrationDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
