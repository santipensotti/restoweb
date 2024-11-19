namespace BlazorAppWithServer.Shared.Models;

public class RestaurantRequest
{
    public string Name { get; set; }                   // Nombre del restaurante
    public double Latitude { get; set; }                // Latitud del restaurante
    public double Longitude { get; set; }               // Longitud del restaurante
    public int TableCount { get; set; }                 // Cantidad de mesas del restaurante
}