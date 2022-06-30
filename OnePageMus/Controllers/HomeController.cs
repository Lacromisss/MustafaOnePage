using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnePageMus.DAL;
using OnePageMus.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnePageMus.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
     
        public IActionResult Index()
        {
           List<Cart> cart = _context.carts.ToList();
            return View(cart);
        }

       

       
    }
}
