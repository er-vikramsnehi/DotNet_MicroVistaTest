using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Models
{
    public class LoginClass
    {
        [Required]
        public string StudentMail { get; set; }
        [Required]
        public string StudentPassword { get; set; }

    }
}
