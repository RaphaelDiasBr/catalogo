using Oportunity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Oportunity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LjShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductContext _context;

        public HomeController(ProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var smartphoneList = await _context.Products.Where(x => x.CategoryId == 1).OrderByDescending(x => x.Id).Take(5).ToListAsync();
            var veiculoList = await _context.Products.Where(x => x.CategoryId == 2).OrderByDescending(x => x.Id).Take(5).ToListAsync();

            var result = new List<ProductViewModel>();

            result.AddRange(smartphoneList);
            result.AddRange(veiculoList);

            if (result == null)
                return View("Error");

            return View(result);
        }

        public async Task<IActionResult> Shopping(int categoryId)
        {
            var result = await _context.Products.ToListAsync();

            if (categoryId > 0)
                result = result.Where(x => x.CategoryId == categoryId).ToList();
            
            if (result == null)
                return View("Error");

            ViewBag.Category = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }
    }
}