using System.Text.Json;

namespace Middlewares.Contracts
{
    public class ResponseError
    {
        public string? StatusCode { get; set; }
        public string[]? Messages { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
