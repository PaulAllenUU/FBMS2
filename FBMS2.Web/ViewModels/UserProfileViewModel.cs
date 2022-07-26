using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using FBMS2.Core.Models;

namespace FBMS2.Web.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }
 
        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmailAvailable", controller: "User", AdditionalFields = nameof(Id))]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}