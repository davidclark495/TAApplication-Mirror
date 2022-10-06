using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TAApplication.Data;
using TAApplication.Models;
using System.Data;
using Microsoft.AspNetCore.Identity;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<TAUser> _um;

        public ApplicationsController(ApplicationDbContext db, UserManager<TAUser> um)
        {
            _db = db;
            _um = um;
        }

        // GET: Applications
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _db.Applications.ToListAsync());
        }

        // GET: Applications/List
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> List()
        {
            return View(await _db.Applications.ToListAsync());
        }

        // GET: Applications/Details/5
        [Authorize(Roles = "Admin,Professor,Applicant")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Applications == null)
            {
                return NotFound();
            }

            var application = await _db.Applications
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> Create()
        {
            TAUser u = await _um.GetUserAsync(User);
            Application existingApp = _db.Applications.Where(app => app.Applicant.Id == u.Id).First();
            if (existingApp != null)
            {
                return RedirectToAction("Details", new { id = existingApp.ID });
            }
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Applicant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PursuingDegree,Program,GPA,HoursWanted,EarlyAvailability,SemestersCompletedAtUtah,PersonalStatement,TransferSchool,LinkedInURL,ResumeFilename")] Application application)
        {
            try
            {
                ModelState.Remove("Applicant");
                ModelState.Remove("CreatedBy");
                ModelState.Remove("ModifiedBy");
                if (ModelState.IsValid)
                {
                    application.Applicant = await _um.GetUserAsync(User);
                    _db.Add(application);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = application.ID });
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(application);
        }



        // GET: Applications/Edit/5
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Applications == null)
            {
                return NotFound();
            }

            var application = await _db.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        [Authorize(Roles = "Applicant")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null) { return BadRequest(); }
            var applicationToUpdate = _db.Applications
                .Where(o => o.ID == id)
                .Include(o => o.Applicant)
                .FirstOrDefault();
            if (applicationToUpdate != null)
            {
                if (await TryUpdateModelAsync<Application>(applicationToUpdate, "",
                                           s => s.PursuingDegree,
                                           s => s.Program,
                                           s => s.GPA,
                                           s => s.HoursWanted,
                                           s => s.EarlyAvailability,
                                           s => s.SemestersCompletedAtUtah,
                                           s => s.PersonalStatement,
                                           s => s.TransferSchool,
                                           s => s.LinkedInURL,
                                           s => s.ResumeFilename
                                           ))
                {
                    try {
                        _db.SaveChanges();
                        return RedirectToAction("Details", new { id = applicationToUpdate.ID });
                    }
                    catch (DataException /* dex */)
                    {
                        // manage error logging
                    }
                }
            }
            return View(applicationToUpdate);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Applications == null)
            {
                return NotFound();
            }

            var application = await _db.Applications
                .FirstOrDefaultAsync(m => m.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            TAUser currUser = await _um.GetUserAsync(User);
            bool userIsAdmin = await _um.IsInRoleAsync(currUser, "Admin");
            if (!userIsAdmin && currUser.Id != application.Applicant.Id)
            {
                return BadRequest(new {message = "You don't own this."});
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [Authorize(Roles = "Admin,Applicant")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Applications == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Applications'  is null.");
            }
            var application = await _db.Applications.FindAsync(id);

            TAUser currUser = await _um.GetUserAsync(User);
            bool userIsAdmin = await _um.IsInRoleAsync(currUser, "Admin");
            if (!userIsAdmin && currUser.Id != application.Applicant.Id)
            {
                return BadRequest(new { message = "You don't own this." });
            }

            if (application != null)
            {
                _db.Applications.Remove(application);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ApplicationExists(int id)
        {
            return _db.Applications.Any(e => e.ID == id);
        }
    }
}
