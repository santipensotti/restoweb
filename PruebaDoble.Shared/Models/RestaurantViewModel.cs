namespace BlazorAppWithServer.Shared.Models
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TablesCount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool ShowDetails { get; set; } = false; // Inicializada en false por defecto
    }
}