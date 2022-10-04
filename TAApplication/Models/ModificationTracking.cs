/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      10/02/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# model for modification tracking.
    Can be inherited by model classes.
 */

using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class ModificationTracking
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        //[DisplayFormat(DateFormat.GeneralDate.ToString("MM/dd/yyyy"))]
        //[DisplayFormat(NullDisplayText = "", DataFormatString = "")] // TODO DateFormat or DateFormatString?
        [ScaffoldColumn(false)]
        public DateTime CreationDate {get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Modification Date")]
        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Created by")]
        [ScaffoldColumn(false)]
        public String CreatedBy { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Modified by")]
        [ScaffoldColumn(false)]
        public String ModifiedBy { get; set; }
    }
}
