using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class HouseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HouseId { get; set; }

        [MaxLength(10)]
        public string House_size { get; set; }

        [MaxLength(25)]
        public string House_type { get; set; }

        [MaxLength(5)]
        [Required]
        public string House_number { get; set; }

        [Required]
        [MaxLength(25)]
        public string Street { get; set; }

        [Required]
        [MaxLength(25)]
        public string Suburb { get; set; }

        [Required]
        [MaxLength(25)]
        public string State { get; set; }

        [Required]
        [MaxLength(5)]
        public string Postal_code { get; set; }

        public string ImgPath { get; set; }

        
        public virtual ICollection<TenantModel> Tenants { get; set; }


    }
}
