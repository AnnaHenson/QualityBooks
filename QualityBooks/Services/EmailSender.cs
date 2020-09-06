using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace QualityBooks.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, string message)
        {
            var mes = new MimeMessage();
            var MyName = "Anna Henson";
            var myEmailAddress = "HENSOA01@myunitec.ac.nz";
            mes.From.Add(new MailboxAddress(MyName, myEmailAddress));
            mes.To.Add(new MailboxAddress("User", email));
            mes.Subject = subject;
            mes.Body = new TextPart("html")
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                // for demo purposes accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.office365.com", 587, false);
                //note : Since we don't have a OAuth2 token, disable the XOAUTH2 authenication token.
                
                client.Authenticate(myEmailAddress, "20111970");

                client.Send(mes);
                client.Disconnect(true);
            }
            // plug in your email service here to send an email.
        }
    }

}         

