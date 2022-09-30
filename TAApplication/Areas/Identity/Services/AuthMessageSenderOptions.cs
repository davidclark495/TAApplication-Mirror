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

		SendGrid user and key getters and setters 
*/

namespace TAApplication.Areas.Identity.Services
{
    public class AuthMessageSenderOptions
    {
        public string? SendGridUser { get; set; }
        public string? SendGridKey { get; set; }
    }
}
