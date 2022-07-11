namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; } = DateTime.Now;
}