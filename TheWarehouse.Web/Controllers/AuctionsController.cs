using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
    public class AuctionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IHostingEnvironment _env;

        #region Paginado 
        private readonly int _RegistrosPorPagina = 4;
        private List<Auction> _Auctions;
        public PaginadorGenerico<Auction> _PaginadorAnnouncements;
        #endregion
        public AuctionsController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> UserManager)
        {
            _context = context;
            _env = env;
            _UserManager = UserManager;
        }

        // GET: Auctions
        public async Task<IActionResult> Index(int id, string buscar, string cantidad1, string cantidad2, int pagina = 1)
        {
            var applicationDbContext = _context.Auction.Include(a => a.ApplicationUser);
            List<TimeSpan> tiempos = new List<TimeSpan>();
            foreach (var item in applicationDbContext)
            {
                if (item.Date.Subtract(DateTime.Now).TotalMilliseconds < 0)
                {
                    List<ApplicationUser> ganador = (from user in _context.ApplicationUsers
                                                     where user.UserName == item.Winner
                                                     select user).ToList();
                    if (item.Winner != null)
                    {
                        ganador[0].MoneyCard -= item.ActualPrice * 0.16;
                        _context.ApplicationUsers.Update(ganador[0]);
                        item.ApplicationUser.MoneyCard += item.ActualPrice;
                        _context.ApplicationUsers.Update(item.ApplicationUser);

                        Sale sale = new Sale();
                        sale.SaleDay = item.Date;
                        sale.Subtotal = item.ActualPrice;
                        sale.Tax = sale.Subtotal * 0.16;
                        sale.Total = sale.Subtotal + sale.Tax;
                        _context.Sales.Add(sale);

                        //pa mannana
                        //sale.SaleList = ( new SaleList{
                        //                                        A = producto.Producto.Id,
                        //                                        Cant = 1,
                        //                                        Total = item.ActualPrice

                        //                              }).ToList();
                        //SaleList saleList = new SaleList();
                        //saleList.Announcement.ProductName = item.ProductName;
                        //saleList.Cant = 1;
                        //saleList.Total = item.ActualPrice;

                    }
                    _context.Auction.Remove(item);
                    _context.SaveChanges();
                }

            }
            

            int _TotalRegistros = 0;
            int _TotalPaginas = 0;
            applicationDbContext = _context.Auction.Include(a => a.ApplicationUser);

            bool busc = string.IsNullOrEmpty(buscar);
            bool cant1 = string.IsNullOrEmpty(cantidad1);
            bool cant2 = string.IsNullOrEmpty(cantidad2);

            if (!busc && !cant1 && !cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Auctions = applicationDbContext.Where(x => x.ActualPrice <= (int.Parse(cantidad2)) &&
                                                  x.ActualPrice >= (int.Parse(cantidad1)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && !cant1 && cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Auctions = applicationDbContext.Where(x => x.ActualPrice >= (int.Parse(cantidad1)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && cant1 && !cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Auctions = applicationDbContext.Where(x => x.ActualPrice <= (int.Parse(cantidad2)) &&
                                                  x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (!busc && cant1 && cant2)
            {
                foreach (var item in buscar.Split(new char[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries))

                {
                    _Auctions = applicationDbContext.Where(x => x.ProductName.Contains(item))
                                                  .ToList();
                }
                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && !cant1 && !cant2)
            {
                _Auctions = applicationDbContext.Where(x => x.ActualPrice <= (int.Parse(cantidad2)) &&
                                              x.ActualPrice >= (int.Parse(cantidad1)))
                                              .ToList();

                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && !cant1 && cant2)
            {
                _Auctions = applicationDbContext.Where(x => x.ActualPrice >= (int.Parse(cantidad1)))
                                              .ToList();

                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else if (busc && cant1 && !cant2)
            {
                _Auctions = applicationDbContext.Where(x => x.ActualPrice <= (int.Parse(cantidad2)))
                                              .ToList();

                _TotalRegistros = _Auctions.Count();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }
            else
            {
                _TotalRegistros = applicationDbContext.Count();
                _Auctions = (List<Auction>)applicationDbContext.ToList();
                _Auctions = _Auctions.OrderBy(x => x.ActualPrice)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();
            }


            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            foreach (var item in _Auctions)
            {
                TimeSpan aux = item.Date.Subtract(DateTime.Now);
                tiempos.Add(new TimeSpan(aux.Days,aux.Hours,aux.Minutes,aux.Seconds));
            }
            ViewBag.tiempos = tiempos;
            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAnnouncements = new PaginadorGenerico<Auction>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscar,
                Resultado = _Auctions
            };


            // Enviamos a la Vista la 'Clase de paginación'


            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");



            return View(_PaginadorAnnouncements);
        }

        // GET: Auctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction
                .Include(a => a.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            int time = (int)auction.Date.Subtract(DateTime.Now).TotalSeconds;
            if (time <= 0)
                return RedirectToAction(nameof(Index));
            ViewBag.time = time;
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auctions/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");
            ViewBag.DateNow = DateTime.Now;
            var applicationDbContext = _context.Auction.Include(a => a.ApplicationUserId);
            return View();
        }

        // POST: Auctions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,ProductName,Winner,InitialPrice,ActualPrice,Date,UploadPicture,Id")] AuctionViewModel auction)
        {
            if (ModelState.IsValid)
            {
                Auction auction1 = new Auction();
                auction1.ActualPrice = auction.InitialPrice;
                auction1.InitialPrice = auction.InitialPrice;
                auction1.Date = auction.Date;
                auction1.Id = auction.Id;
                auction1.ApplicationUserId = _UserManager.GetUserId(User);
                auction1.ProductName = auction.ProductName;
                auction1.Winner = null;

                var uniqueFileName = GetUniqueFileName(auction.UploadPicture.FileName);
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                auction.UploadPicture.CopyTo(new FileStream(filePath, FileMode.Create));

                auction1.Picture = uniqueFileName;
                _context.Add(auction1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", _UserManager.GetUserId(User));
            return View(auction);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        // GET: Auctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction.SingleOrDefaultAsync(m => m.Id == id);
            ViewBag.Date = auction.Date;
            if (auction == null)
            {
                return NotFound();
            }

            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", auction.ApplicationUserId);

            return View(auction);
        }

        // POST: Auctions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId,ProductName,Winner,InitialPrice,ActualPrice,Date,Picture,Id")] Auction auction)
        {
            Auction auction1 = _context.Auction.Find(id);
            auction1.ProductName = auction.ProductName;
            auction1.Date = auction.Date;
            if (id != auction.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auction1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", auction.ApplicationUserId);
            return View(auction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int id, [Bind("ApplicationUserId,ProductName,Winner,InitialPrice,ActualPrice,Date,Picture,Id")] Auction auction)
        {
            Auction auction1 = _context.Auction.Find(id);
            auction1.Winner = _UserManager.GetUserName(User);
            auction1.ActualPrice = auction.ActualPrice;

            if (id != auction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auction1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", auction.ApplicationUserId);
            return View(auction1);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auction
                .Include(a => a.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auction = await _context.Auction.SingleOrDefaultAsync(m => m.Id == id);
            _context.Auction.Remove(auction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionExists(int id)
        {
            return _context.Auction.Any(e => e.Id == id);
        }
    }
}
