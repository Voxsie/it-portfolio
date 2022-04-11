using MimeKit;

namespace Portfolio.Misc.Services;

public interface IEmailSender
{
    public void SendEmail(string mesBody, string mesHeader, string mesReciever);
}