using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student_MVC.Models
{
    public class student
    {
        [Key]
        public int studentID { get; set; }

        [Required]
        [DisplayName("Student name")]
        public string studentName { get; set; }

        [Required]
        [DisplayName("Course")]
        public string course { get; set; }

        [Required]
        [DisplayName("Department")]
        public string department { get; set; }
    }
}