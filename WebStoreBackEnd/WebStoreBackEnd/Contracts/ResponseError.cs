using System.Text.Json;

namespace WebStoreBackEnd.Contracts
{
    public class ResponseError
    {
        public string? StatusCode { get; set; }
        public string[]? Messages { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
