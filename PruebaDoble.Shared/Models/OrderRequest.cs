namespace BlazorAppWithServer.Shared.Models
{
    public class OrderRequest
    {
        public int RestoId { get; set; }
        public int TableId { get; set; }
        public MenuItem MenuItem { get; set; } = new MenuItem();
        public DateTime RequestTime { get; set; } = DateTime.Now;
        public bool IsDelivered { get; set; } = false;
    }
}
