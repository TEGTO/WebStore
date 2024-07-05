namespace WebStoreApi.Domain.Dtos
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public float AvgRating { get; set; }
        public string? ImgUrl { get; set; }
    }
}
