using MediatR;

namespace CleanArchitecture.Application.Notifications.EmailNotifications
{
    public class SendEmailNotification : INotification
    {
        public string Email { get; set; }
    }
}
