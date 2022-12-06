/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      12/04/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# model for tracking Course-Enrollment data over time.  
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TAApplication.Models
{
    public class EnrollmentRecord
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "")]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 300)]
        [Display(Name = "Currently Enrolled", ShortName = "Enrolled")]
        public int Enrollment { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Required]
        public Course Course { get; set; } = null!;
    }
}
