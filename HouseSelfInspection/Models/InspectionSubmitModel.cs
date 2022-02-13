using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class InspectionSubmitModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InspectionUploadId { get; set; }

        public int HouseId { get; set; }

        public string UserId { get; set; }

        public int SectionId { get; set; }

        public int ImageId { get; set; }

        public DateTime InspectionSubmittedDate { get; set; }
    }
}
