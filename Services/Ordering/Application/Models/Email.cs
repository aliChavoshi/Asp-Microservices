namespace Application.Models;

public abstract class Email
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}