using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SocialNetwork.Resources;

namespace SocialNetwork.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Resource), Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class EditAccountViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase UserPhoto { get; set; }
    }

    public class ViewAccountViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public double UserRate { get; set; }
        public int TaskAmount { get; set; }
        public int AttemptAmount { get; set; }
        public int SolutionAmount { get; set; }
        public string UserPhotoUrl { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutDateEndUtc { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class MySolvedTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int AttemptAmount { get; set; }
    }

    public class MyTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public DateTime DateAdded { get; set; }
        public bool TaskStatus { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "Current_password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "ChangePasswordViewModel_NewPassword_The__0__must_be_at_least__2__characters_long_", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "New_password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "Confirm_new_password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "NotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Resource), Name = "User_Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Remember")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(ResourceType = typeof(Resource), Name = "User_Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Resource), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "ChangePasswordViewModel_NewPassword_The__0__must_be_at_least__2__characters_long_", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "NotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Resource), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "ChangePasswordViewModel_NewPassword_The__0__must_be_at_least__2__characters_long_", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "NotMatch")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Resource), Name = "Email")]
        public string Email { get; set; }
    }
}
