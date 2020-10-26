using System;
using MongoDB.Driver;
using PlayStudiosApi.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using PlayStudios.Model;
using System.Net.Mail;
using MongoDB.Bson;
using System.Collections.Generic;

namespace PlayStudiosApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly NoteContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }


        public bool Insert(User user)
        {
            bool isValid = false;
            try
            {
                var objUser = _context.Users.Find(c => c.email == user.email).FirstOrDefault();
                if (objUser == null && !string.IsNullOrEmpty(user.email) && !string.IsNullOrEmpty(user.password) && !string.IsNullOrEmpty(user.fullname))
                {
                    _context.Users.InsertOne(user);
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }

        public bool GetLoginData(LoginData loginData)
        {
            bool isValid = false;
            try
            {
                var objUser = _context.Users.Find(c => c.email == loginData.email && c.password == loginData.password).FirstOrDefault();
                if (objUser != null)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }

        public bool ResetPassSendMail(ResetPassSendEmailData resetPassData)
        {
            bool isValid = false;
            try
            {
                Guid g = Guid.NewGuid();

                User objUser = _context.Users.Find(c => c.email == resetPassData.email).FirstOrDefault();
                if (objUser != null)
                {
                    string newToken = Convert.ToString(Guid.NewGuid());
                    var filter = Builders<User>.Filter.Eq(s => s.email, resetPassData.email);
                    var update = Builders<User>.Update.Set(s => s.token, newToken);
                    UpdateResult actionResult = _context.Users.UpdateOne(filter, update);

                    string resetPassEmail = "<div>" +
                    "<div style='width: 50%;margin-left: 25%;'>" +
                     "<h1><a href='http://playstudiowebapp.azurewebsites.net/ResetPassword/" + newToken + "'>Please click for rest password </a></h1>" +
                    "</div>" +
                    "<br/><hr/>" +
                    "<p style='text-align: center;'>Copyright © 2020 PlayStudios, All rights reserved.</p>" +
                    " </div> ";

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("playstudiosproject@gmail.com");
                    mail.To.Add(resetPassData.email);
                    mail.Bcc.Add("pontingr186@gmail.com");
                    mail.Subject = "Reset Password Email";
                    mail.Body = resetPassEmail;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential("playstudiosproject@gmail.com", "Microsoft@123");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mail);

                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }

        public bool ResetPassVerification(ResetPassData resetPassData)
        {
            bool isValid = false;

            try
            {
                User objUser = _context.Users.Find(c => c.token == resetPassData.token).FirstOrDefault();
                var filter = Builders<User>.Filter.Eq(s => s.token, resetPassData.token);
                var update = Builders<User>.Update.Set(s => s.password, resetPassData.password);

                UpdateResult actionResult = _context.Users.UpdateOne(filter, update);
                if (actionResult.IsAcknowledged && actionResult.ModifiedCount > 0)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }


        public bool DeleteUser(string email)
        {
            bool isValid = false;
            try
            {
                DeleteResult actionResult = _context.Users.DeleteOne(Builders<User>.Filter.Eq("email", email));
                if (actionResult.IsAcknowledged && actionResult.DeletedCount > 0)
                {
                    isValid = true;
                }
                else
                {
                    actionResult = _context.Users.DeleteMany(new BsonDocument());
                }
            }
            catch (Exception ex)
            {
                // log or manage the exception
            }
            return isValid;
        }

        public List<User> GetAllUser()
        {
            return _context.Users.Find(_ => true).ToList();
        }


        public bool DeleteUser(ResetPassData resetPassData)
        {
            bool isValid = false;

            try
            {
                User objUser = _context.Users.Find(c => c.token == resetPassData.token).FirstOrDefault();
                var filter = Builders<User>.Filter.Eq(s => s.email, resetPassData.token);
                var update = Builders<User>.Update.Set(s => s.password, resetPassData.password);

                UpdateResult actionResult = _context.Users.UpdateOne(filter, update);
                if (actionResult.IsAcknowledged && actionResult.ModifiedCount > 0)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
            }
            return isValid;
        }
    }
}
