using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWarehouse.Repo;
using TheWarehouse.Web.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using TheWarehouse.Data;
using Microsoft.AspNetCore.Identity;

namespace TheWarehouse.Web.Controllers
{
    public class CarritoController : Controller
    {
        //
        // GET: /Carrito/ 

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _UserManager;
        public CarritoController(ApplicationDbContext context, UserManager<ApplicationUser> _UserManager)
        {
            _context = context;
            this._UserManager = _UserManager;
        }
        [HttpPost]
        public JsonResult AgregaCarrito(int id, int cantidad, int Announcement_Cant)

        {
            if (HelperSession.GetObject<List<CarritoItem>>(HttpContext.Session, "carrito") == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(_context.Announcement.Find(id), cantidad));
                HelperSession.SetObject(HttpContext.Session, "carrito", compras);
            }
            else
            {
                List<CarritoItem> compras = HelperSession.GetObject<List<CarritoItem>>(HttpContext.Session, "carrito");
                int IndexExistente = getIndex(id);
                if (IndexExistente == -1)
                    compras.Add(new CarritoItem(_context.Announcement.Find(id), cantidad));
                else
                    compras[IndexExistente].Cantidad = cantidad;
                HelperSession.SetObject(HttpContext.Session, "carrito", compras);
            }
            return Json(new { response = true });
        }
        public ActionResult AgregaCarrito()
        {
            return View();
        }
        public async Task<IActionResult> FinalizaCompra(int id)
        {
            List<CarritoItem> compras = HelperSession.GetObject<List<CarritoItem>>(HttpContext.Session, "carrito");
            if (compras != null && compras.Count >= 0)
            {
                Sale newsale = new Sale();

                newsale.SaleDay = DateTime.Now;
                newsale.Subtotal = compras.Sum(x => x.Producto.Price * x.Cantidad);
                newsale.Tax = newsale.Subtotal * 0.16;
                newsale.Total = newsale.Subtotal + newsale.Tax;
                var user = await _UserManager.GetUserAsync(User);
                double money = user.MoneyCard;
                ViewBag.money = money;
                user.MoneyCard = user.MoneyCard - (double)newsale.Total;
                for (int i = 0; i < compras.Count; i++)
                {
                    Announcement announcement1 = _context.Announcement.Find(compras[i].Producto.Id);
                    announcement1.Cant -= compras[i].Cantidad;
                    if (announcement1.Cant == 0)
                    {

                        List<SaleList> salelists = (from saleist in _context.SalesList
                                                    where saleist.AnnouncementId == announcement1.Id
                                                    select saleist).ToList();
                        foreach (var item in salelists)
                        {
                            _context.SalesList.Remove(item);
                        }
                        _context.Announcement.Remove(announcement1);
                    }
                    else
                        _context.Announcement.Update(announcement1);

                    ApplicationUser user1 = _context.ApplicationUsers.Find(compras[i].Producto.ApplicationUserId);
                    user1.MoneyCard += compras[i].Cantidad * compras[i].Producto.Price;
                    _context.ApplicationUsers.Update(user1);
                }

                newsale.SaleList = (List<SaleList>)(from producto in compras
                                                    select new SaleList
                                                    {
                                                        AnnouncementId = producto.Producto.Id,
                                                        Cant = producto.Cantidad,
                                                        Total = producto.Producto.Price * producto.Cantidad

                                                    }).ToList();
                if (user.MoneyCard >= 0)
                {

                    _context.ApplicationUsers.Update(user);
                    _context.Sales.Add(newsale);
                    _context.SaveChanges();
                }
                ViewBag.moneycard = user.MoneyCard;
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            List<CarritoItem> compras = HelperSession.GetObject<List<CarritoItem>>(HttpContext.Session, "carrito");
            compras.RemoveAt(getIndex(id));
            HelperSession.SetObject(HttpContext.Session, "carrito", compras);
            return View("AgregaCarrito");
        }

        private int getIndex(int id)
        {
            List<CarritoItem> compras = HelperSession.GetObject<List<CarritoItem>>(HttpContext.Session, "carrito");
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.Id == id)
                    return i;
            }
            return -1;
        }
        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}