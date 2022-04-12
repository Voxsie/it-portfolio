using MimeKit;

namespace Portfolio.Misc.Services;

public class Message
{ 
    public MimeMessage MimeMessage { get; set; }
    
    public EmailConfiguration Configuration;

    public Message(string header, string content, string reciever, EmailConfiguration emailConfiguration)
    {
        Configuration = emailConfiguration;
        MimeMessage = CreateMessage(header, CreateBody(content));
        MimeMessage.To.Add(new MailboxAddress($"{reciever}", $"{reciever}"));
    }
    
    public MimeMessage CreateMessage(string header, MimeEntity bodyEntity)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress(Configuration.UserName, Configuration.From ));
        message.Subject = header;
        message.Body = bodyEntity;
        return message;
    }

    public MimeEntity CreateBody(string bodyText)
        => new BodyBuilder() {TextBody = $"{bodyText}"}.ToMessageBody();
}