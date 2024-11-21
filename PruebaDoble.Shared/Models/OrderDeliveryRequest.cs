namespace BlazorAppWithServer.Shared.Models
{
    public class OrderDeliveryRequest
    {
        public int RestoId { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public OrderState NewState { get; set; }

    }
}
