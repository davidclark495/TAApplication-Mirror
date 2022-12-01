using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAApplication.Areas.Identity.Data;
using TAApplication.Data;
using TAApplication.Models;

namespace TAApplication.Controllers
{
    [Authorize]
    public class AvailabilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<TAUser> _um;

        public AvailabilityController(ApplicationDbContext db, UserManager<TAUser> um)
        {
            _db = db;
            _um = um;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Replaces the existing slots for the current user
        /// </summary>
        /// <param name="newSlots">Complete set of slots for a TA schedule</param>
        [HttpPost]
        public async Task SetSchedule( string[] newSlots)
        {
            // Gets and deletes all previous slots
            TAUser currUser = await _um.GetUserAsync(User);
            var oldSlots = _db.Slots.Where(slot => slot.TAUserId == currUser.Id);
            //foreach (var slot in oldSlots)
            //    _db.Slots.Remove(slot);

            // Adds new schedule (in the form of slots)
            //foreach (Slot slot in newSlots)
            //    await _db.Slots.AddAsync(slot);

            _db.SaveChanges();
        }

        /// <summary>
        /// Returns an array of Slots for a TA User
        /// </summary>
        [HttpGet]
        public async Task<Slot[]> GetSchedule()
        {
            TAUser currUser = await _um.GetUserAsync(User);
            Slot[] oldSlots = _db.Slots.Where(slot => slot.TAUserId == currUser.Id).ToArray();
            return oldSlots;
        }
    }
}
