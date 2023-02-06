using Oportunity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oportunity.Context;
using Microsoft.EntityFrameworkCore;

namespace LjShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ProductContext _context;
    private string _filePath;

    public ProductsController(ProductContext context, IWebHostEnvironment env)
    {
        _context = context;
        _filePath = env.WebRootPath;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _context.Products.ToListAsync();

        if (result == null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetails(int id)
    {
        //ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");

        var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM, IFormFile image)
    {
        if (ValidaImagem(image) && ModelState.IsValid)
        {
            productVM.ImageURL = SalvarArquivo(image);
            _context.Products.Add(productVM);           

            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");
        }       

        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");

        var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            _context.Update(productVM);
           
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return RedirectToAction(nameof(Index));
        }
        return View(productVM);
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        if (id == 0)
        {
            return View("Error");
        }

        var produto = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null)
        {
            return View("Error");
        }

        return View(produto);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Products.FindAsync(id);
        
        _context.Products.Remove(produto);
        
        var result = await _context.SaveChangesAsync();

        if (result == 0)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }

    private bool ValidaImagem(IFormFile image)
    {
        switch (image.ContentType)
        {
            case "image/jpeg":
                return true;
            case "image/jpg":
                return true;
            case "image/png":
                return true;
            default:    
                return false;
        }
    }

    private string SalvarArquivo(IFormFile image)
    {
        var nome = Guid.NewGuid().ToString() + image.FileName;
        var filePath = _filePath + "\\images";

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        using (var stream = System.IO.File.Create(filePath + "\\" + nome))
        {
            image.CopyToAsync(stream);
        }

        return nome;
    }
}