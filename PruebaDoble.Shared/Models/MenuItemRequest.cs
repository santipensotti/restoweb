namespace BlazorAppWithServer.Shared.Models;

public class MenuItemRequest
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
} 