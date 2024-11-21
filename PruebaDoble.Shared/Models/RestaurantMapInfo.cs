using System.Text.Json.Serialization;

namespace BlazorAppWithServer.Shared.Models
{
    public class RestaurantMapInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tablesCount")]
        public int TablesCount { get; set; }
    }
}
