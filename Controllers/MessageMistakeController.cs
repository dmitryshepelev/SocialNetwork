using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Controllers
{
    public class MessageMistakeController : Controller
    {
        private ApplicationUserManager userManager;
        public MessageMistakeController()
        {
        }

        public MessageMistakeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public void SendMessage(int taskId)
        {
            if (ModelState.IsValid)
            {
                var ac = new ApplicationDbContext();
                var userTask = ac.UserTasks.Find(taskId);
                var user = new ApplicationUser() { UserName = userTask.User.UserName, Email = userTask.User.Email};
                
                var from = "polli.simple@gmail.com";
                var pass = "pollisimple";

                // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(from, pass);
                client.EnableSsl = true;
                // создаем письмо: message.Destination - адрес получателя
                var mail = new MailMessage(from, user.Email);
                mail.Subject = "You have a mistake";
                mail.Body = "Please correct this task: " + userTask.UserTaskTitle;
                mail.IsBodyHtml = true;

                client.Send(mail);
                mail.Dispose();
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


    }
}