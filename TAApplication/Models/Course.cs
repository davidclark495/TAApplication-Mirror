/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      10/19/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# model for Courses
 */

using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TAApplication.Models
{
    public enum Semester { Fall, Spring, Summer }
    public class Course
    {
        [Required]
        [Display(Name = "Semester Offered", ShortName = "Semester")]
        public Enum Semester { get; set; }


        [Required]
        [Range(0, 10000)]
        [Display(Name = "Year Offered", ShortName = "Year")]
        public int Year { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Course Name", ShortName = "Title")]
        public string Title { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Department Offering", ShortName = "Dept")]
        public string Department { get; set; }


        [Required]
        [Range(0, 9999)]
        [Display(Name = "Course Number", ShortName = "Course No")]
        public int Number { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Section")]
        public string Section { get; set; }


        [Required]
        [StringLength(1000)]
        [Display(Name = "Course Description", ShortName = "Description")]
        public string Description { get; set; }


        [Required]
        [RegularExpression(@"u[0-9]{7}$")]
        [Display(Name = "Professor ID")]
        public int ProfessorUNID { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Professor Name", ShortName = "Professor")]
        public string ProfessorName { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Days and Times Offered", ShortName = "Dates")]
        public string TimeAndDaysOffered { get; set; }


        [Required]
        [StringLength(150)]
        [Display(Name = "Location")]
        public string Location { get; set; }


        [Required]
        [Range(0, 6)]
        [Display(Name = "Credit Hours", ShortName = "Credits")]
        public int CreditHours { get; set; }


        [Required]
        [Range(0, 300)]
        [Display(Name = "Currently Enrolled", ShortName = "Enrolled")]
        public int Enrollment { get; set; }


        [Required]
        [StringLength(1000)]
        [Display(Name = "Admin Note")]
        public string Note { get; set; }
    }
}