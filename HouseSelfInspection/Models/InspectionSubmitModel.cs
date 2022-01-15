using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class InspectionSubmitModel
    {
        public int InspectionUploadId { get; set; }

        public int HouseId { get; set; }

        public int TenantId { get; set; }

        public int SectionId { get; set; }

        public int ImageId { get; set; }

        public DateTime InspectionSubmittedDate { get; set; }
    }
}
