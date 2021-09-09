using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class SendNotificationEmailRequest
    {
        public string MailFrom { get; set; }
        public List<string> MailTo { get; set; }
        public string MailBody { get; set; }
        public string MailCC { get; set; }
        public string Subject { get; set; }
        public string MailWebsite { get; set; }
        public EmailConfig Config { get; set; }
        public EmailCredentials Credentials { get; set; }
    }

    public class NotificationRequest
    {
        public NotificationTemplates NotificationTemplates { get; set; }
        public List<User> User { get; set; }
    }

    public class EmailCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class EmailConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }

    }

    public class NotificationTemplates
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public int NumberOfRecipient { get; set; }
        public string DefaultEmail { get; set; }
        public string DefaultPhoneNumber { get; set; }
        public int NotificationTemplateTypeID { get; set; }
    }
}
