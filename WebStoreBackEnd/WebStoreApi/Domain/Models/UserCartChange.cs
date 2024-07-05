namespace WebStoreApi.Domain.Models
{
    public class UserCartChange
    {
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
