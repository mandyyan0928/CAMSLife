using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models
{
    public class EmailSetting
    {

        public string Username => ConfigurationManager.AppSettings["EmailUser"] ?? "cams@caliphgroup.com";//"cams@caliphgroup.com";
        public string Host => ConfigurationManager.AppSettings["EmailServer"] ?? "smtp.gmail.com";
        public string Password => ConfigurationManager.AppSettings["EmailPass"] ?? "ungb erdq bcdv oyqr";//"TF3am<TW3<mA7mC>";
        public string Port => ConfigurationManager.AppSettings["EmailPort"] ?? "587";
    }

    public class EmailHistory { 
    
    }

    public class EmailSender
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}