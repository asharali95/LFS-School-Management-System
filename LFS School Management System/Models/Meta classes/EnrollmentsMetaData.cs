using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LFS_School_Management_System.Models
{
    public class EnrollmentsMetaData
    {
        [Display(Name ="Student Grade")]
        public Nullable<decimal> Grade { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Student")]
        public int StudentID { get; set; }

        [Display(Name = "Student")]
        public Student Student { get; set; }

        [Display(Name = "Lecturer")]
        public Nullable<int> LecturerId { get; set; }


        [Display(Name = "Lecturer")]
        public Lecturer Lecturer { get; set; }
    }


    [MetadataType(typeof(EnrollmentsMetaData))]
    public partial class Enrollment{
        

     }
}