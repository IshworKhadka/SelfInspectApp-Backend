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
        public int TenantId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string Contact { get; set; }

        [Required]
        [MaxLength(25)]
        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        [ForeignKey("HouseId")]
        public virtual HouseModel House { get; set; }

        public int HouseId { get; set; }

        [MaxLength(100)]
        public string house_address { get; set; }


    }
}
