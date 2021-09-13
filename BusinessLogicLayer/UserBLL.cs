using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using System.Net.Mail;
using System.Net;
using DataAccessLayer.Context;

namespace BusinessLogicLayer
{
    public class UserBLL
    {
        private readonly IMongoCollection<User> db;
        public UserBLL(IUserDbContext user)
        {
            var client = new MongoClient(user.ConnectionString);
            var database = client.GetDatabase(user.DatabaseName);

            db = database.GetCollection<User>(user.CollectionName);
        }

        public User Register(User user)
        {
            user.UserID = (int)db.Find(user => true)
                .CountDocuments() + 1;
            db.InsertOne(user);
            user.isRegistered = true;
            user.ClientState = (int)Client.Registered;
            return user;
        }

        public static bool SendNotificationEmail(SendNotificationEmailRequest request)
        {
            bool isSuccess = false;
            try
            {
                SmtpClient smtpClient = new SmtpClient(request.Config.Host, request.Config.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(request.Credentials.UserName, request.Credentials.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = false
                };
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(request.MailFrom, request.MailWebsite);
                foreach (var item in request.MailTo)
                {
                    mail.To.Add(new MailAddress(item));
                }
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                mail.Subject = request.Subject;
                mail.Body = request.MailBody;
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);
                isSuccess = true;
                return isSuccess;
            }
            catch (Exception ex)
            {

                return isSuccess;
            }
        }
    }
}
