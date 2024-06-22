namespace WebStoreBackEnd.Models
{
    public class AccessToken
    {
        public string? Token { get; set; }
        public DateTime ExpiredOn { get; set; }

        public AccessToken(string? token, DateTime expiredOn)
        {
            Token = token;
            ExpiredOn = expiredOn;
        }
    }
}
