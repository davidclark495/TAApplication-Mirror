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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task SetRole(int unid, string role)
        {
            TAUser? user = _db.Users.Where(u=>u.Unid == unid).FirstOrDefault();

            if (user != null)
            {
                var roles = await _um.GetRolesAsync(user);
                await _um.RemoveFromRolesAsync(user, roles.ToArray());
                await _um.AddToRoleAsync(user, role);
            }
        }
    }
}
