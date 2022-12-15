using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWarehouse.Repo;

namespace TheWarehouse.Web.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ApplicationUser
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationUsers;
            return View(await applicationDbContext.ToListAsync());
        }
        

        // GET: ApplicationUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}