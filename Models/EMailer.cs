using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace HealthCare_ProtoType.Models
{
    internal class EMailer
    {
        string MyEmailMail = "priyanshu76670@gmail.com";
        string MyMailAppPassword = "dpei xoad detv qiem";

        public bool SendMyEmail(string SendTo, string Message)
        {
            try
            {
                // Step 1 : Perform setting of message.

                MailMessage msg = new MailMessage();
                MailAddress maFrom = new MailAddress(MyEmailMail);
                msg.From = maFrom;
                //msg.Subject = Subject;
                msg.To.Add(SendTo);
                msg.Body = Message;

                // Step 2 : Create and set SMTP Protocol

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(MyEmailMail, MyMailAppPassword);
                client.Credentials = nc;

                // Step 3 : send Mail
                client.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}