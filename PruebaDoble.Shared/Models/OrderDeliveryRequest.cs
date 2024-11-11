namespace BlazorAppWithServer.Shared.Models;

public class OrderDeliveryRequest
{
    public int TableId { get; set; }
    public string Order { get; set; } = string.Empty;
} 