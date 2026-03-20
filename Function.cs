using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using ms_notifications.Models;
using ms_notifications.Handlers;

namespace ms_notifications;

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
      if (emailEvent == null)
        throw new Exception("Invalid message body");
      await _handler.Handle(emailEvent, context);
    }
  }
}