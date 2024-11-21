namespace BlazorAppWithServer.Shared.Models;

public class RestaurantRequest
{
    public string Name { get; set; }                 
    public double Latitude { get; set; }             
    public double Longitude { get; set; }            
    public int TableCount { get; set; }
}