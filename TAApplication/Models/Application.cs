/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      10/02/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# model for Applications
 */
using System.ComponentModel.DataAnnotations;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Models
{
    public enum DegreeLevel { BS, MS, PhD }

    public class Application : ModificationTracking
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Pursuing Degree", ShortName = "Degree")]
        public DegreeLevel PursuingDegree { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Program enrolled in", ShortName = "Program", Prompt = "", Description = "Department and Program pursuing")]
        public String Program { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        [Display(Name = "Grade Point Average", ShortName = "GPA")]
        public double GPA { get; set; }

        [Required]
        [Range(5, 20)]
        [Display(Name = "Hours Wanted", ShortName = "Hours", Prompt = "", Description = "Number of hours the applicant wants to work per week")]
        public int HoursWanted { get; set; }

        [Required]
        [Display(Name = "Early Availability", ShortName = "Can Start Early", Prompt = "", Description = "Is the applicant available for work a week before the class start date")]
        public bool EarlyAvailability { get; set; }

        [Required]
        [Range(0, 999)]
        [Display(Name = "Semesters Completed at U of U", ShortName = "Semesters Completed", Prompt = "", Description = "Number of semesters the applicant has completed at the U of U")]
        public int SemestersCompletedAtUtah { get; set; }

        [StringLength(50000)]
        [Display(Name = "Personal Statement", ShortName = "Statement", Prompt = "", Description = "Applicant's personal statement")]
        public String? PersonalStatement { get; set; }

        [StringLength(150)]
        [Display(Name = "Transfer School", ShortName = "Transfered from", Prompt = "", Description = "Name of the school the applicant has transfered from")]
        public String? TransferSchool { get; set; }

        [Url]
        [StringLength(300)]
        [Display(Name = "LinkedIn Profile", ShortName = "LinkedIn", Prompt = "", Description = "Applicant's LinkedIn social media profile url")]
        public String? LinkedInURL { get; set; }

        [StringLength(300)]
        [Display(Name = "Upload Resume", ShortName = "Resume", Prompt = "", Description = "Applicant's resume")]
        public String? ResumeFilename { get; set; }

        // Navigation Properties
        [Required]
        public TAUser Applicant { get; set; } = null!;
    }
}
