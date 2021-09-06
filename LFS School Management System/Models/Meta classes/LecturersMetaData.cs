using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LFS_School_Management_System.Models
{
    public class LecturersMetaData
    {
        [Required]
        [Display(Name ="First Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }
    }

    [MetadataType(typeof(LecturersMetaData))]
    public partial class Lecturer
    {

    }
}
