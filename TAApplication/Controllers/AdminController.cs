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

		Controls access to admin pages. Facilitates role management.
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TAApplication.Areas.Identity.Data;
using TAApplication.Data;

namespace TAApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
	{
        private readonly UserManager<TAUser> _um;
        private readonly ApplicationDbContext _db;
        public AdminController(UserManager<TAUser> um, ApplicationDbContext db)
        {
            _um = um;
            _db = db;
        }
        public IActionResult Roles()
        {
            return View();
        }
        [HttpPost]
        public async Task SetRole(int unid, string role)
        {
            TAUser? user = _db.Users.Where(u=>u.Unid == unid).FirstOrDefault();

            if (user != null)
            {
                var roles = (await _um.GetRolesAsync(user)).ToArray();
                
                await _um.RemoveFromRolesAsync(user, roles);
                await _um.AddToRoleAsync(user, role);
            }
        }
    }
}
