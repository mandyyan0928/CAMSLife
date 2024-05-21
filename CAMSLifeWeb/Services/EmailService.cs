using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Services
{
    public class EmailService
    {
        public EmailService()
        {


        }

        //public async Task SendEmailAsync(EmailHistory emailHistory)
        //{
        //    try
        //    {
        //        MailMessage msgMail = new MailMessage();
        //        msgMail.From = new MailAddress(_emailSetting.Username);
        //        msgMail.Subject = emailHistory.Subject;
        //        msgMail.BodyEncoding = System.Text.Encoding.UTF8;

        //        msgMail.IsBodyHtml = true;
        //        msgMail.To.Add(emailHistory.MailTo);
        //        msgMail.Body = emailHistory.Content;

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

        //        SmtpClient smtpClient = new SmtpClient(_emailSetting.Host, _emailSetting.Port);
        //        smtpClient.EnableSsl = false;
        //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtpClient.UseDefaultCredentials = false;
        //        smtpClient.Credentials = new NetworkCredential(_emailSetting.Username, _emailSetting.Password);

        //        await Task.Run(() => smtpClient.Send(msgMail));
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggerService.LogError("SendEmailAsync", ex.Message, "");
        //    }
        //}
    }
}