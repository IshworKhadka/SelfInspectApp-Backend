using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class TenantModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TenantId")]
        public int TenantId { get; set; }

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

        [Required(ErrorMessage = "Password is a required field.")]
        [MaxLength(25, ErrorMessage = "Maximum length for Password is 25 characters.")]
        public string Password { get; set; }

        [ForeignKey("HouseId")]


        public virtual HouseModel House { get; set; }

        public int HouseId { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length for House Address is 100 characters.")]
        public string house_address { get; set; }


    }
}
