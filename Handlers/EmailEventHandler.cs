using Amazon.Lambda.Core;
using ms_notifications.Models;
using ms_notifications.Services;

namespace NotificationLambda.Handlers;

public class EmailEventHandler
{
  private readonly EmailService _service;

  public EmailEventHandler()
  {
    _service = new EmailService();
  }

  public async Task Handle(EmailEvent messageBody, ILambdaContext context)
  {
    context.Logger.LogInformation($"Processando mensagem: {messageBody.Title} email: {messageBody.Recipient}");

    await _service.SendEmailAsync(messageBody);
  }
}