/*
	Author: Robert Davidson
	Partner: David Clark
	Date: 10/02/2022
	Course: CS 4540, University of Utah, School of Computing
	Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
	
	I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
	another source.  Any references used in the completion of the assignment are cited in my README file.

	I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
	another source. Any references used in the completion of the assignment are cited in my README file.

	File Contents

		OLD - Did provide access to old web forms based on user role
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAApplication.Models;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Controllers
{
    [Authorize]
    public class OLDController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<TAUser> _um;

        public OLDController(ILogger<HomeController> logger, UserManager<TAUser> um)
        {
            _logger = logger;
            _um = um;
        }

        public IActionResult ApplicationCreate()
        {
            return View();
        }

        public IActionResult ApplicationDetails()
        {
            TAUser currUser = _um.GetUserAsync(User).Result;
            int unid = currUser.Unid;
            if (User.IsInRole("Admin") || User.IsInRole("Professor") || unid == 0000000)
                return View();
            else
                return LocalRedirect("/Identity/Account/AccessDenied");
        }

        public IActionResult ApplicationList()
        {
            return View();
        }
    }
}