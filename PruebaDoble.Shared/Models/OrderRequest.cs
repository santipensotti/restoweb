namespace BlazorAppWithServer.Shared.Models;

public class OrderRequest
{
    public int TableId { get; set; }
    public string Order { get; set; } = string.Empty;
    public DateTime RequestTime { get; set; } = DateTime.Now;
    public bool IsDelivered { get; set; } = false;
} 