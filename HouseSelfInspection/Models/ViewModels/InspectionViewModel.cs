using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class InspectionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InspectionScheduleId { get; set; }

       
        public int HouseId { get; set; }

        public string HouseAddress { get; set; }

        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        public DateTime Inspection_date { get; set; }

        public string Inspection_status { get; set; }
    }
}
