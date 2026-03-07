using System.Net;
using System.Net.Mail;
using ms_notifications.Models;

namespace ms_notifications.Services
{
  public class EmailService
  {
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task SendEmailAsync(EmailEvent emailEvent)
    {
      try
      {
        var smtpServer = _configuration["Email:SmtpServer"] ?? throw new InvalidOperationException("SMTP server not configured");
        var smtpPort = _configuration["Email:SmtpPort"] ?? throw new InvalidOperationException("SMTP port not configured");
        var smtpUser = _configuration["Email:SmtpUser"] ?? throw new InvalidOperationException("SMTP user not configured");
        var smtpPass = _configuration["Email:SmtpPass"] ?? throw new InvalidOperationException("SMTP password not configured");

        using var client = new SmtpClient(smtpServer)
        {
          Port = int.Parse(smtpPort),
          Credentials = new NetworkCredential(smtpUser, smtpPass),
          EnableSsl = true
        };

        var htmlBody = BuildTemplate(emailEvent);

        var mail = new MailMessage
        {
          From = new MailAddress(emailEvent.Sender ?? smtpUser, "Game Store"),
          Subject = emailEvent.Title,
          Body = htmlBody,
          IsBodyHtml = true
        };

        mail.To.Add(emailEvent.Recipient);

        await client.SendMailAsync(mail);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public string BuildTemplate(EmailEvent emailEvent)
    {
      return $@"
        <!DOCTYPE html>
        <html lang='pt-br'>
        <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>{emailEvent.Title}</title>
        </head>

        <body style='font-family: Arial, sans-serif; background-color:#f4f4f4; margin:0; padding:0;'>

        <table width='100%' cellpadding='0' cellspacing='0'>
        <tr>
        <td align='center' style='padding:40px 0; background-color:#ffffff;'>

        <img src='https://www.pngplay.com/wp-content/uploads/8/Phoenix-Fire-Transparent-Free-PNG.png'
        width='120'
        style='display:block;margin:auto;' />

        </td>
        </tr>

        <tr>
        <td align='center' style='padding:40px 20px;background-color:#ffffff;'>

        <h2 style='color:#333'>{emailEvent.Title}</h2>

        <h4 style='color:#666'>{emailEvent.Subtitle}</h4>

        <p style='font-size:16px;color:#555;max-width:500px'>
        {emailEvent.Body}
        </p>

        </td>
        </tr>

        <tr>
        <td align='center' style='padding:20px;background-color:#f4f4f4;color:#999;font-size:12px'>
        © {DateTime.Now.Year} GameStore
        </td>
        </tr>

        </table>

        </body>
        </html>";
    }
  }
}