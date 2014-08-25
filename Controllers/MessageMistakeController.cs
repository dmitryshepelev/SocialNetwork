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
        private IUserTaskRepository userTaskRepositrory;
        public MessageMistakeController()
        {
        }

        public MessageMistakeController(IUserTaskRepository userTaskRepositrory)
        {
            this.userTaskRepositrory = userTaskRepositrory;
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
                var userTask = userTaskRepositrory.GetById(taskId);
                var user = new ApplicationUser() { UserName = userTask.User.UserName, Email = userTask.User.Email};
                
                var from = "polli.simple@gmail.com";
                var pass = "pollisimple";
                var client = new SmtpClient("smtp.gmail.com", 25)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(@from, pass),
                    EnableSsl = true
                };
                var mail = new MailMessage(from, user.Email)
                {
                    Subject = "You have a mistake",
                    Body = "Please correct this task: " + userTask.UserTaskTitle,
                    IsBodyHtml = true
                };

                client.Send(mail);
                mail.Dispose();
            }
        }
    }
}