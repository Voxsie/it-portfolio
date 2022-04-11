using MimeKit;

namespace Portfolio.Misc.Services;

public interface IEmailSender
{
    public void SendEmail(string mesBody, string mesHeader);
    public MimeMessage CreateMessage(string header, MimeEntity bodyEntity);
    public MimeEntity CreateBody(string bodyText);
}