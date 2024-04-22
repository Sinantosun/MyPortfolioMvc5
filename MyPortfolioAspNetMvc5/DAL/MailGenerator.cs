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

            MailboxAddress mailAddresFrom = new MailboxAddress("Cv Admin", "sinantosuncvsite@gmail.com");
            mimeMessage.From.Add(mailAddresFrom);

            MailboxAddress mailAdressTo = new MailboxAddress("Sinan Tosun", "sinan_tosun4141@hotmail.com");
            mimeMessage.To.Add(mailAdressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "Yeni Mesajınız!! Var<br> " + nameSurname.ToUpper() + " Size Mesaj Gönderdi"+ "<br>  Lütfen Siteden Kontrol edin.!";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = subject;


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("sinantosuncvsite@gmail.com", "lhtn sndy dqqs mncm");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return true;
        }
    }
}