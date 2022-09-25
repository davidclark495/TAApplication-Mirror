/*
	Author: Robert Davidson
	Partner: David Clark
	Date: 09/23/2022
	Course: CS 4540, University of Utah, School of Computing
	Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
	
  	I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
  	another source.  Any references used in the completion of the assignment are cited in my README file.

	I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
	another source. Any references used in the completion of the assignment are cited in my README file.

	File Contents

		TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO 
*/


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TAApplication.Areas.Identity.Data
{
    public class TAUser : IdentityUser
    {
        // TODO: Index IsUnique? How to enforce uniquness on a unid
        //[Index(nameof(Unid), IsUnique = true)]
        [RegularExpression(@"u[0-9]{7}$")] // TODO: Test this regex
        [Display(Name = "U of U uID")]
        [Required()]
        public int Unid { get; set; }

        [Required()]
        public string Name { get; set; }

        public string ReferredTo { get; set; }

        [EmailAddress]
        [Required()]
        public override string Email { get; set; }

    }
}