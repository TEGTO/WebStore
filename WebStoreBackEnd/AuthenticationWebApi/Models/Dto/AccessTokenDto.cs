﻿namespace AuthenticationWebApi.Models.Dto
{
    public class AccessTokenDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }
}
