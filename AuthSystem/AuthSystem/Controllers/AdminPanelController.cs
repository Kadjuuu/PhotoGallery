using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Controllers

{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private readonly AuthDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminPanelController(AuthDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UserModel = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (UserModel == null)
            {
                return NotFound();
            }

            return View(UserModel);
        }
        private bool UserModelExist(int id)
        {
            return _context.Users.Any(e => Convert.ToInt32(e.Id) == id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccConfirmation(string id)
        {
            var AppUser = await _context.Users.FindAsync(id);

            _context.Users.Remove(AppUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UserModel = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (UserModel == null)
            {
                return NotFound();
            }

            return View(UserModel);
        }



    }
}
