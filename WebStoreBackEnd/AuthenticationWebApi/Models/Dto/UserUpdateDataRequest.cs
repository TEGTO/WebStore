namespace AuthenticationWebApi.Models.Dto
{
    public class UserUpdateDataRequest
    {
        public string? OldEmail { get; set; }
        public string? NewEmail { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
