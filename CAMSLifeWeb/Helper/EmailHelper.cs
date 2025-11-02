using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Net.Mail;
using CaliphWeb.Models;
using CaliphWeb.Models.API;

namespace CaliphWeb.Helper
{
    public class EmailHelper
    {

        public void SendEmail(EmailSender email)
        {
            try
            {
                var setting = new EmailSetting();

                var fromAddress = new MailAddress(setting.Username, "Caliph Group");
                var toAddress = new MailAddress(email.Email);

                var smtp = new SmtpClient
                {
                    Host =setting.Host,
                    Port = ConvertHelper.ConvertInt(setting.Port, 587),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, setting.Password)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml=true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public  EmailSender GetRegisterEmail(UserRequest user, string pass )
        {

            var content = @"<table style=""border: none;background:white;"">
    <tbody>
        <tr>
            <td style=""padding:0cm 0cm 0cm 0cm;""><br></td>
        </tr>
        <tr>
            <td style=""padding:0cm 0cm 0cm 0cm;"">
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:41px;font-family:""Segoe UI Light"",sans-serif;color:#2672EC;'>Welcome to Caliph Group</span></p>
            </td>
        </tr>
        <tr>
            <td style=""padding:18.75pt 0cm 0cm 0cm;"">
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Your agent account for Caliph Activity Management System has been created.&nbsp;</span></p>
            </td>
        </tr>
        <tr>
            <td style=""padding:18.75pt 0cm 0cm 0cm;height:103.75pt;"">
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>You may login to with below credentials to start your activity management&nbsp;</span></p>
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>&nbsp;</span></p>
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Login URL :&nbsp;</span>
              <a href=""{{url}}"" target=""_blank""> {{url}} </a>
              </p>
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Login ID :</span> {{agentid}}</p>
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Login Password :</span> {{password}}</p>
            </td>
        </tr>
        <tr>
            <td style=""padding:18.75pt 0cm 0cm 0cm;"">
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Thanks,</span></p>
            </td>
        </tr>
        <tr>
            <td style=""padding:0cm 0cm 0cm 0cm;"">
                <p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'><span style='font-size:14px;font-family:""Segoe UI"",sans-serif;color:#2A2A2A;'>Caliph Group</span></p>
            </td>
        </tr>
    </tbody>
</table>
<p style='margin:0cm;font-size:16px;font-family:""Calibri"",sans-serif;'>&nbsp;</p>";


            var subject = "Caliph Activity Mangement System Agent Account";

            return new EmailSender { Body = content
                .Replace("{{url}}", System.Configuration.ConfigurationManager.AppSettings["LoginUrl"]?? "https://login.caliphgroup.com")
                .Replace("{{agentid}}", user.Username)
                .Replace("{{password}}", pass), Subject = subject, Email = user.Email };
        }
    }
}