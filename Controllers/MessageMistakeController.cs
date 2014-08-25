using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult> SendMessage(int messageMistake)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext ac = new ApplicationDbContext();
                var userTask = ac.UserTasks.Find(messageMistake);
                var user = new ApplicationUser() { UserName = userTask.User.UserName, Email = userTask.User.Email};
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ViewTask", "UserTask", new {userId = user.Id, code = code},
                        protocol: Request.Url.Scheme);
                    await
                        UserManager.SendEmailAsync(user.Id, "Confirm your account",
                            "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return View("SendMessage");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View();
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