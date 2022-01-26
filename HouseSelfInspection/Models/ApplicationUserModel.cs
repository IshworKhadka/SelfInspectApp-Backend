using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class ApplicationUserModel: IdentityUser
    {

        [Required(ErrorMessage = "Tenant name is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for the Name is 25 characters.")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    
        public int HouseId { get; set; }

        public int RoleId { get; set; }

        public string ImagePath { get; set; }
    }
}
