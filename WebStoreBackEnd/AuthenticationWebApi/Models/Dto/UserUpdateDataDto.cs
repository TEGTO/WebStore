namespace AuthenticationWebApi.Models.Dto
{
    public class UserUpdateDataDto
    {
        public string? OldEmail { get; set; }
        public string? NewEmail { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
