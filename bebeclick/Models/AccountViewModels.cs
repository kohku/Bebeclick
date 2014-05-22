using System;
using System.ComponentModel.DataAnnotations;

namespace Bebeclick.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        public string Name { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "FirstNameRequired", ErrorMessage = "")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "LastNameRequired", ErrorMessage = "")]
        public string LastName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "GenderRequired", ErrorMessage = "")]
        public string Gender { get; set; }

        [Display(Name = "BirthDay", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "BirthDayRequired", ErrorMessage = "")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "EmailRequired", ErrorMessage = "")]
        [EmailAddress(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "InvalidEmail", ErrorMessage = "")]
        public string Email { get; set; }

        [Display(Name = "City", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "CityRequired", ErrorMessage = "")]
        public string City { get; set; }

        [Display(Name = "State", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "StateRequired", ErrorMessage = "")]
        public string State { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "EmailRequired", ErrorMessage = "")]
        [EmailAddress(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "InvalidEmail", ErrorMessage = "")]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "EmailRequired", ErrorMessage = "")]
        [EmailAddress(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "InvalidEmail", ErrorMessage = "")]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "PasswordRequired", ErrorMessage = "")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "PasswordLength", ErrorMessage = "", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Bebeclick.WebClient.Resources.Account))]
        [Required(ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "ConfirmPasswordRequired", ErrorMessage = "")]
        [Compare("Password", ErrorMessageResourceType = typeof(Bebeclick.WebClient.Resources.Account), ErrorMessageResourceName = "ComparePassword", ErrorMessage = "")]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
