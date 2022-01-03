using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Models
{
    public class ViewModel
    {

        [Key]
        public int StudentId { get; set; }
        public string StudentMail { get; set; }
       public string StudentName { get; set; }
        public string StudentDOB { get; set; }
        public string StudentAddress { get; set; }
        public string StudentImage { get; set; }
        public string StudentPassword { get; set; }










        [Key]
        public int StudentPostId { get; set; }
        public int StudentPostProfileName { get; set; }
        public int StudentPostProfileImage { get; set; }
        public int StudentPostSummary { get; set; }
        public int StudentPostImage { get; set; }


        [DisplayName("Role")]
        public string UserRole { get; set; }


        [DisplayName("UserRole")]
        [Required(ErrorMessage = "Enter the UserRole")]
        public Role EUserRole { get; set; }


    }






    public enum Role
    {
        Student,
        Admin
    }



}
