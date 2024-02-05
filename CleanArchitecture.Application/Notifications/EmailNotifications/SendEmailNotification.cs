namespace CleanArchitecture.Application.Notifications.EmailNotifications;

public class SendEmailNotification : BaseEvent
{
    public SendEmailNotification(string email)
    {
        Email = email;
    }
    public string Email { get; private set; }
}
