using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models.ViewModels
{
    public class FeedbackViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InspectionId { get; set; }

        public int SectionId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public string FeedbackGivenBy { get; set; }

        public DateTime FeedbackDate { get; set; }
    }
}
