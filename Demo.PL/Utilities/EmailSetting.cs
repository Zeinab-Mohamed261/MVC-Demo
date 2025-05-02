using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utilities
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com" , 587);
            client.EnableSsl = true;//certificate
            client.Credentials = new NetworkCredential("zm664278@gmail.com", "nria dluh jkoh yfgy");
            client.Send("zm664278@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
