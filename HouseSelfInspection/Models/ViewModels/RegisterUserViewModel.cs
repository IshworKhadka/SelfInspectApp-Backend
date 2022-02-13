using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Tenant name is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for the Name is 25 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for the Contact is 25 characters.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for Email is 25 characters.")]
        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Username is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for Username is 25 characters.")]
        public string Username { get; set; }

       
        [MaxLength(25, ErrorMessage = "Maximum length for Password is 25 characters.")]
        public string Password { get; set; }

        public int HouseId { get; set; }

        public string ImagePath { get; set; }
    }
}
