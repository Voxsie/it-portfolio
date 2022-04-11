using Microsoft.Extensions.Logging;
using MimeKit;

namespace Portfolio.Misc.Services;

public class EmailSender : IEmailSender
{
    public readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public MimeMessage CreateMessage(string header, MimeEntity bodyEntity)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("отправитель", "whoomipark@gmail.com" ));
        message.To.Add(new MailboxAddress("получатель", "tyurina.vladislava@gmail.com"));
        message.Subject = header;
        message.Body = bodyEntity;
        return message;
    }

    public MimeEntity CreateBody(string bodyText)
    {
        var temp = new BodyBuilder();
        temp.HtmlBody = $"<div style=\"color: black;\">{bodyText}</div>";
        return temp.ToMessageBody();
    }
    
    public void SendEmail(string mesBody, string mesHeader)
    {
        MimeMessage message = CreateMessage(mesHeader, CreateBody(mesBody));
        try
        {
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("whoomipark@gmail.com", "pass");
                client.Send(message);
                client.Disconnect(true);
                _logger.LogInformation("successfully sent");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}