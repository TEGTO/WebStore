namespace WebStoreApi.Dtos.ServiceDtos
{
    public class UserCartChangeServiceRequest
    {
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
