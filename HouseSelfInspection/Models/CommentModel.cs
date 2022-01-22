using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Models
{
    public class CommentModel
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        public int ActivityId { get; set; }

        public string CommentDesciption { get; set; }

        public int CommentUserId { get; set; }

        public DateTime CommentDateTime { get; set; }

    }
}
