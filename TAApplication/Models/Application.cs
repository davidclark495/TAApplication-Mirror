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

namespace TAApplication.Models
{
    public class Application : ModificationTracking
    {
        // TODO
        [DisplayFormat(NullDisplayText = "", DataFormatString = "")] // TODO DateFormat or DateFormatString?
        [DataType(DataType.DateTime)]
        [StringLength(50000)]
        [Url]
        // END TODO
        [Required]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public Enum CurrentDegree { get; set; }
        
        [Required]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public String Program { get; set; }
        
        [Required]
        [Range(0.0,4.0)]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public double GPA { get; set; }
        
        [Required]
        [Range(5,20)]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public int HoursWanted { get; set; }
        
        [Required]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public bool EarlyAvailability { get; set; }
        
        [Required]
        [Range(0,999)]
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public int SemestersCompletedAtUtah { get; set; }
        
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public String? PersonalStatement { get; set; }
        
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public String? TransferSchool { get; set; }
        
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public String? LinkedInURL { get; set; }
        
        [Display(Name = "", ShortName = "", Prompt = "", Description = "")]
        public String? ResumeFilename { get; set; }
        //TODO: Creation Date
        //TODO: Modification Date
    }
}
