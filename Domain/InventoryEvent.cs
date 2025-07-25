namespace Domain;

public class InventoryEvent
{
    public int Id { get; set; }
    public string EventType { get; set; } = string.Empty; // Created, Updated, Deleted
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
