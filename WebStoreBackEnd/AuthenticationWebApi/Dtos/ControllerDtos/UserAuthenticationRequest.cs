namespace AuthenticationWebApi.Dtos.ControllerDtos
{
    public class UserAuthenticationRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
