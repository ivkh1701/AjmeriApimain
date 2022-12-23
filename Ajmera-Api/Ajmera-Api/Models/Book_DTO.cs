using Newtonsoft.Json;

namespace Ajmera_Api.Models
{
    public class Book_DTO
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("authorName")]
        public string? AuthorName { get; set; }
    }
}
