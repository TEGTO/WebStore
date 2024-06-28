namespace WebStoreApi.Models.Dto
{
    public class UserCartChangeDto
    {
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
