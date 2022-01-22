using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.Static_Models
{
    public class HouseTypeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HouseTypeId { get; set; }

        [Required]
        public string HouseType { get; set; }
    }
}
