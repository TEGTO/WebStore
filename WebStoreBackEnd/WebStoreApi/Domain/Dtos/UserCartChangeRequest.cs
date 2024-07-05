namespace WebStoreApi.Domain.Dtos
{
    public class UserCartChangeRequest
    {
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
