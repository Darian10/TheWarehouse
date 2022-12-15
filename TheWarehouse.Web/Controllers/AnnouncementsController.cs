using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheWarehouse.Data;
using TheWarehouse.Repo;
using TheWarehouse.Web.Models;
using TheWarehouse.Web.Models.Paginado;

namespace TheWarehouse.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        #region Paginado 
        private readonly int _RegistrosPorPagina = 8;
        private List<Announcement> _Announcements;
        public PaginadorGenerico<Announcement> _PaginadorAnnouncements;
        #endregion
        private readonly UserManager<ApplicationUser> _UserManager;
        public AnnouncementsController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _env = env;
            _context = context;
            _UserManager = userManager;
        }

        // GET: Announcement
        public ActionResult Index(int id, string buscar, string cantidad1, string cantidad2, int pagina = 1)
        {
            var applicationDbContext = _context.Announcement.Include(a => a.ApplicationUser);


            int _TotalRegistros = 0;
            int _TotalPaginas = 0;
            bool busc = string.IsNullOrEmpty(buscar);
            bool cant1 = string.IsNullOrEmpty(cantidad1);
            bool cant2 = string.IsNullOrEmpty(cantidad2);

            #region filtros
            if (!busc && !cant1 && !cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Announcements = applicationDbContext.Where(x => x.Cant <= (int.Parse(cantidad2)) &&
                                                  x.Cant >= (int.Parse(cantidad1)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && !cant1 && cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Announcements = applicationDbContext.Where(x => x.Cant >= (int.Parse(cantidad1)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && cant1 && !cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Announcements = applicationDbContext.Where(x => x.Cant <= (int.Parse(cantidad2)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && cant1 && cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Announcements = applicationDbContext.Where(x => x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && !cant1 && !cant2)
            {
                    _Announcements = applicationDbContext.Where(x => x.Cant <= (int.Parse(cantidad2)) &&
                                                  x.Cant >= (int.Parse(cantidad1))) 
                                                  .ToList();
                                                  
                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && !cant1 && cant2)
            {
                _Announcements = applicationDbContext.Where(x => x.Cant >= (int.Parse(cantidad1)))
                                              .ToList();

                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && cant1 && !cant2)
            {
                _Announcements = applicationDbContext.Where(x => x.Cant <= (int.Parse(cantidad2)))                                   
                                              .ToList();

                _TotalRegistros = _Announcements.Count();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else
            {
                _TotalRegistros = applicationDbContext.Count();
                _Announcements = (List<Announcement>)applicationDbContext.ToList();
                _Announcements = _Announcements.OrderBy(x => x.Price)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            #endregion

            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAnnouncements = new PaginadorGenerico<Announcement>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscar,
                Resultado = _Announcements
            };


            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAnnouncements);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,ProductName,Price,Cant,UploadPicture,VigencyDate,Id")] AnnouncementViewModel announcement)
        {
            if (ModelState.IsValid)
            {
                Announcement Announcement1 = new Announcement();
                Announcement1.ProductName = announcement.ProductName;
                Announcement1.Price = announcement.Price;
                Announcement1.Cant = announcement.Cant;
                Announcement1.Id = announcement.Id;
                Announcement1.ApplicationUserId = _UserManager.GetUserId(User);
                Announcement1.VigencyDate = announcement.VigencyDate;


                AnnouncementSaleList AnnouncementSaleList = new AnnouncementSaleList();
                AnnouncementSaleList.ProductName = announcement.ProductName;
                AnnouncementSaleList.Price = announcement.Price;
                AnnouncementSaleList.Cant = announcement.Cant;
                AnnouncementSaleList.Id = announcement.Id;
                AnnouncementSaleList.ApplicationUserId = _UserManager.GetUserId(User);
                AnnouncementSaleList.VigencyDate = announcement.VigencyDate;

                var uniqueFileName = GetUniqueFileName(announcement.UploadPicture.FileName);
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                announcement.UploadPicture.CopyTo(new FileStream(filePath, FileMode.Create));

                Announcement1.Foto = uniqueFileName;
                _context.Add(Announcement1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", _UserManager.GetUserId(User));
            return View(announcement);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", announcement.ApplicationUserId);
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId,ProductName,Price,Cant,Foto,VigencyDate,Id")] Announcement announcement)
        {
            Announcement announcement1 = _context.Announcement.Find(id);
            announcement1.ProductName = announcement.ProductName;
            announcement1.Price = announcement.Price;
            announcement1.Cant = announcement.Cant;

            AnnouncementSaleList AnnouncementSaleList = _context.AnnouncementSaleList.Find(id);
            AnnouncementSaleList.ProductName = announcement.ProductName;
            AnnouncementSaleList.Price = announcement.Price;
            AnnouncementSaleList.Cant = announcement.Cant;
            if (id != announcement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(AnnouncementSaleList);
                    _context.Update(announcement1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", announcement.ApplicationUserId);
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .Include(a => a.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcement.SingleOrDefaultAsync(m => m.Id == id);
            //List<SaleList> listaventas = (from salelist in _context.SalesList
            //                              where salelist.AnnouncementId == id
            //                              select salelist).ToList();
            //foreach (var item in listaventas)
            //{
            //    _context.SalesList.Remove(item);
            //}
            _context.Announcement.Remove(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcement.Any(e => e.Id == id);
        }
    }
}
