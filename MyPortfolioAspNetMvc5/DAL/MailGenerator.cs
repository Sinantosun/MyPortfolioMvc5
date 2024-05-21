using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortfolioAspNetMvc5.DAL
{
    public class MailGenerator
    {
        public bool SendMail(string nameSurname,string subject,string content)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailAddresFrom = new MailboxAddress("", "");
            mimeMessage.From.Add(mailAddresFrom);

            MailboxAddress mailAdressTo = new MailboxAddress("", "");
            mimeMessage.To.Add(mailAdressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "Yeni Mesajınız Var!! <br><br> " + nameSurname.ToUpper() + " Size " + subject.ToUpper() + " Konu Başlıklı bir mesaj gönderdi detayları için admin panelinden kontrol edin.";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = subject;


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("", "");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return true;
        }
    }
}