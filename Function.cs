using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using ms_notifications.Models;
using NotificationLambda.Handlers;

namespace NotificationLambda;

public class Function
{
  private readonly EmailEventHandler _handler;

  public Function()
  {
    _handler = new EmailEventHandler();
  }

  public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
  {
    foreach (var message in evnt.Records)
    {
      var emailEvent = JsonSerializer.Deserialize<EmailEvent>(message.Body);
      await _handler.Handle(emailEvent, context);
    }
  }
}