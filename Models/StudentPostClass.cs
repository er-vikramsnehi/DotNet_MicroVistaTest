using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Models
{
    public class StudentPostClass
    {
        [Key]
        public int StudentPostId { get; set; }
        public string StudentPostProfileName { get; set; }
        public string StudentPostProfileImage { get; set; }
        public string StudentPostSummary { get; set; }
        public string StudentPostImage { get; set; }


    }
}
