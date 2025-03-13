using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace webdoc.Models
{
    public class BookingEmailSender
    {
        private string MyEmailId = "arpits2305@gmail.com";
        private string MyEmailAppPassword = "wsdqyyvobrbuylbx";

        public bool SendMyEmail(string SendTo, string Subject, string Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(SendTo);
                mail.From = new MailAddress(MyEmailId);
                mail.Subject = Subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(MyEmailId, MyEmailAppPassword);         // Enter seders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}