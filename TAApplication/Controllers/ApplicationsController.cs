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
using Microsoft.EntityFrameworkCore.Storage;

namespace TAApplication.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<TAUser> _um;
        private readonly IConfiguration _configuration;

        public ApplicationsController(ApplicationDbContext db, UserManager<TAUser> um, IConfiguration config)
        {
            _db = db;
            _um = um;
            _configuration = config;
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
            Application? existingApp = _db.Applications.Where(app => app.TAUser.Id == u.Id).FirstOrDefault();
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
                    application.TAUser = await _um.GetUserAsync(User);
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files, string category, int applicationID)
        {
            try
            {
                category = category.ToUpper();
                // (1) check for valid category (resume/photo) and application ID
                Application? app = _db.Applications.Where(a => a.ID == applicationID).FirstOrDefault();
                if (app == null)
                {
                    return BadRequest(new { message = "Application not found." });
                }
                if (category != "RESUME" && category != "IMAGE")
                {
                    ViewData["ErrorMessage"] = "File type not supported. Please try again.";
                    return View("Details", applicationID);
                }
                // (2) check that current user _owns_ application
                TAUser applicant = await _um.GetUserAsync(User);
                if ( applicant.Id != app.TAUserId)
                {
                    ViewData["ErrorMessage"] = "Not authorized to modify this application.";
                    return View("Details", applicationID);
                }
                // (3) check that only one file has been submitted
                if (files.Count != 1)
                {
                    ViewData["ErrorMessage"] = "One file at a time please.";
                    return View("Details", applicationID);
                }
                // (4) check that the file size is less than, say, 10 million bytes
                // (5) check that the file size is greater than 0
                if (files[0].Length > 10000000 || files[0].Length < 0)
                {
                    ViewData["ErrorMessage"] = "Files must be less than 10 megabytes.";
                    return View("Details", applicationID);
                }
                // (6) check that resumes end with .pdf and images end with .png (or other image extensions)
                string fileName = files[0].FileName.ToLower();
                if (!(fileName.EndsWith(".pdf") && category == "RESUME") && !(fileName.EndsWith(".png") && category == "IMAGE"))
                {
                    ViewData["ErrorMessage"] = "File type not supported. Please try again.";
                    return View("Details", applicationID);
                }

                // Actually handle the file //
                // rename the file
                string newName = Path.GetRandomFileName().Replace(".", "");
                // store file in filesystem
                string path = Path.Combine(_configuration["FilePath"], newName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await files[0].CopyToAsync(stream);
                }
                // store filename in database
                if(category == "RESUME")
                {
                    app.ResumeFilename = path;
                }
                else if (category == "IMAGE")
                {// field doesn't currently exist, could be added in future

                }
                _db.SaveChanges();

                return View("Details", applicationID); 
            }
            catch
            {
                return BadRequest(new { message = "How did you get here?" });
            }
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
                .Include(o => o.TAUser)
                .FirstOrDefault();
            if (applicationToUpdate != null)
            {
                if (await TryUpdateModelAsync<Application>(applicationToUpdate, "",
                                           s => s.PursuingDegree,
                                           s => s.Program,
                                           s => s.GPA,
                                           s => s.HoursWanted,
                                           s => s.EarlyAvailability,
                                           s => s.SemestersCompletedAtUtah/*,
                                           s => s.PersonalStatement,
                                           s => s.TransferSchool,
                                           s => s.LinkedInURL,
                                           s => s.ResumeFilename*/
                                           ))
                {
                    try
                    {
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
            if (!userIsAdmin && currUser.Id != application.TAUser.Id)
            {
                return BadRequest(new { message = "You don't own this." });
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
            if (!userIsAdmin && currUser.Id != application.TAUser.Id)
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
