using Amazon.Lambda.SQSEvents;
using ms_notifications.Models;
using ms_notifications.Services;
using System.Text.Json;

namespace ms_notifications.Handlers
{
  public class EmailEventHandler
  {
    private readonly EmailService _emailService;

    public EmailEventHandler(EmailService emailService)
    {
      _emailService = emailService;
    }

    public async Task Handle(SQSEvent evnt)
    {
      foreach (var message in evnt.Records)
      {
        var emailEvent = JsonSerializer.Deserialize<EmailEvent>(message.Body);

        if (emailEvent != null)
        {
          await _emailService.SendEmailAsync(emailEvent);
        }
      }
    }
  }
}
