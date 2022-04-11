using Microsoft.Extensions.Logging;
using MimeKit;

namespace Portfolio.Misc.Services;

public class EmailSender : IEmailSender
{
    public readonly ILogger<EmailSender> _logger;
    public EmailConfiguration _emailConfiguration;

    public EmailSender(ILogger<EmailSender> logger, EmailConfiguration emailConfiguration)
    {
        _logger = logger;
        _emailConfiguration = emailConfiguration;
    }
    

    public void SendEmail(string mesBody, string mesHeader)
    {
        var message = new Message(mesHeader, mesBody);
        using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(message.MimeMessage);
                client.Disconnect(true);
                _logger.LogInformation("successfully sent");
            }
            catch (Exception e)
            {
              _logger.LogError(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
}
}