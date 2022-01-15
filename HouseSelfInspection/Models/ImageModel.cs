using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }

        public string ImageUrl { get; set; }

        public int SubmittedBy { get; set; }

        public DateTime SubmittedDate { get; set; }

        public int HouseId { get; set; }

        public int SectionId { get; set; }
    }
}
