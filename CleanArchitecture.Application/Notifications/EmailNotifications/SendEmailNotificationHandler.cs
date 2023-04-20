using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Notifications.EmailNotifications
{
    public class SendEmailNotificationHandler : INotificationHandler<SendEmailNotification>
    {
        private readonly ILogger<SendEmailNotificationHandler> _logger;

        public SendEmailNotificationHandler(ILogger<SendEmailNotificationHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
        {
            //Other validations

            //Call others handlers

            //Insert into data base

            return Task.Run(() => _logger.LogInformation("Successful email event. To: {Email}", notification.Email));
        }
    }
}