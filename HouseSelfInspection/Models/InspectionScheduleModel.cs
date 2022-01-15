using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class InspectionScheduleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InspectionScheduleId { get; set; }


        [Required]
        public int HouseId { get; set; }


        [Required]
        public int TenantId { get; set; }

        [Required]
        public DateTime Inspection_date { get; set; }

        public string Inspection_status { get; set; }
    }
}
