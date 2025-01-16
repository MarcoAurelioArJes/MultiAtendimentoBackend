using System.Net;
using System.Net.Mail;

namespace MultiAtendimento.API.Services
{
    public class EmailService
    {
        public static void EnviarEmail(string[] emails, string assunto, string corpoEmail)
        {
            var client = new SmtpClient("live.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("api", "8bb8e3cbeb200af4e701373624be6227"),
                EnableSsl = true
            };

            MailMessage message = new MailMessage
            {
                From = new MailAddress("hello@demomailtrap.com"),
                IsBodyHtml = corpoEmail.ToLower().Contains("<html"),
                Subject = assunto,
                Body = corpoEmail
            };

            foreach (string email in emails)
                message.To.Add(new MailAddress(email));

            client.Send(message);
        }
    }
}