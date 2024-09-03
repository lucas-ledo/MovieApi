using System.Text.Json.Serialization;

namespace MovieApi.Models.TMDB
{
    public class SearchResultTMDB<T> where T : class
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("results")]
        public T? Results { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
