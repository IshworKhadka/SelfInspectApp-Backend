using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class ActivityModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityDescription { get; set; }
        public int ActivityUser { get; set; }
        public int ActivityAudienceUser { get; set; } 
        public DateTime ActivityDate { get; set; }
    }
}
