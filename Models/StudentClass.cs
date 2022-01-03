using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Models
{
    public class StudentClass
    {

        [Key]
        public int StudentId { get; set; }
        
        public string StudentMail { get; set; }

        public string StudentName { get; set; }
       
        public string StudentDOB { get; set; }
       
        public string StudentAddress { get; set; }
      

        public string StudentPassword { get; set; }

        public string StudentImage { get; set; }

        [DisplayName("Role")]
        public string UserRole { get; set; }


    }
}
