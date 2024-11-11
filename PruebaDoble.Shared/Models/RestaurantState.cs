namespace BlazorAppWithServer.Shared.Models;

public class RestaurantState
{
    public int Id { get; set; }
    public List<Table> Tables { get; set; } = new List<Table>();
    public List<MenuItem> Menu { get; set; } = new List<MenuItem>();
} 