using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LFS_School_Management_System.Models
{
    public class StudentMetaData
    {
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [StringLength(50)]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
    }


    [MetadataType(typeof(StudentMetaData))]
    public partial class Student   {    }
}