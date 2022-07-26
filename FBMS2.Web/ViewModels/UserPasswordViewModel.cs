using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FBMS2.Web.ViewModels
{
    public class UserPasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [Remote(action: "VerifyPassword", controller: "User")]
        public string OldPassword { get; set; }
        
        [Required]
        [Compare("OldPassword", ErrorMessage = "New password canont be the same as the old passowrd")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string PasswordConfirm  { get; set; }

    }
}