using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OnePageMus.DAL;
using OnePageMus.Models;
using OnePageMus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnePageMus.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index(int page=1)
        {
            int pageCount = _context.carts.Count();
            if (page<1 || page>pageCount)
            {
                page = 1;
            }
            List<Cart> carts = _context.carts.Skip((page-1)*2).Take(2).ToList();
            PaginationVm<Cart> paginationVm = new PaginationVm<Cart>
            {
                Item = carts,
                PageCount = pageCount,
                ActivPage= page


            };
            return View(paginationVm);
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool check = _context.carts.Any(x => x.Title == cart.Title);
            if (check)
            {
                ModelState.AddModelError("Title", "eyni basliq adda cart yarada bilmezsen");
                return View();

            }
            _context.carts.Add(cart);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            Cart cart = _context.carts.Find(Id);
            if (cart==null)
            {
                return BadRequest();

            }

            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int Id,Cart cart)
        {
            Cart cart2 = _context.carts.Find(Id);
            if (cart == null)
            {
                return NotFound();

            }
            cart2.Description1 = cart.Description1;
            cart2.Description2 = cart.Description2;
            cart2.Title = cart.Title;
            cart2.Icon = cart.Icon;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            Cart cart = _context.carts.Find(Id);
            if (cart==null)
            {
                return BadRequest();

            }
            _context.carts.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
