/*
 Author:    Robert Davidson
 Partner:   David Clark
 Date:      10/19/2022
 Course:    CS 4540, University of Utah, School of Computing
 Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.


 I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.
 I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of the assignment are cited in my README file.


 File Contents
	This is the C# Controller for Courses
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Identity.Data;
using TAApplication.Data;
using TAApplication.Models;

namespace TAApplication.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<TAUser> _um;

        public CoursesController(ApplicationDbContext db, UserManager<TAUser> um)
        {
            _db = db;
            _um = um;
        }

        // GET: Courses/List
        [Authorize(Roles = "Admin, Professor")]
        public async Task<IActionResult> List()
        {
            return View(await _db.Courses.ToListAsync());
        }

        [Authorize(Roles = "Admin, Professor")]
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Courses == null)
            {
                return NotFound();
            }

            var course = await _db.Courses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Semester,Year,Title,Department,Number,Section,Description,ProfessorUNID,ProfessorName,TimeAndDaysOffered,Location,CreditHours,Enrollment,Note")] Course course)
        {
            if (ModelState.IsValid)
            {
                _db.Add(course);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Courses == null)
            {
                return NotFound();
            }

            var course = await _db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Semester,Year,Title,Department,Number,Section,Description,ProfessorUNID,ProfessorName,TimeAndDaysOffered,Location,CreditHours,Enrollment,Note")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(course);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Courses == null)
            {
                return NotFound();
            }

            var course = await _db.Courses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
            }
            var course = await _db.Courses.FindAsync(id);
            if (course != null)
            {
                _db.Courses.Remove(course);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool CourseExists(int id)
        {
          return _db.Courses.Any(e => e.ID == id);
        }
    }
}