﻿namespace AuthenticationWebApi.Models.Dto
{
    public class AuthResponse

    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
