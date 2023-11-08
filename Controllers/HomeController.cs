using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Product> allProducts = _context.Products.ToList();
        ViewBag.AllProducts = allProducts;
        return View();
    }

    [HttpGet]
    [Route("categories")]
    public IActionResult Categories()
    {
        List<Category> allCategories = _context.Categories.ToList();
        ViewBag.AllCategories = allCategories;
        return View("Categories");
    }
    [HttpPost]
    [Route("new/product")]
    public IActionResult NewProduct(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }
    
    [HttpPost]
    [Route("new/category")]

    public IActionResult NewCategory(Category newCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }
        return View("Categories");
    }
    
    [HttpGet]
    [Route("products/{id_product}")]

    public IActionResult Products(int id_product)
    {
        Product? product = _context.Products
            .Include(p => p.Associat)
            .ThenInclude(prod => prod.Category)
            .FirstOrDefault(p => p.ProductId == id_product);
        ViewBag.Product = product;
        var allCategories = _context.Categories.ToList();
        var selectedCategories = product.Associat.Select(a => a.Category).ToList();
        var unselectedCategories = allCategories.Except(selectedCategories).ToList();
        ViewBag.Categories = selectedCategories;
        ViewBag.AllCategories = unselectedCategories;
        return View("Products");
    }
    
    [HttpPost]
    [Route("add/category")]

    public IActionResult AddCategory(Association NewAssociation)
    {
        _context.Associations.Add(NewAssociation);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("/categories/{id}")]
    public IActionResult Categori(int id)
    {
        ViewBag.Category = _context.Categories.Include(a => a.Associations).ThenInclude(c => c.Product).FirstOrDefault(p => p.CategoryId == id);
        ViewBag.Products = _context.Products.ToList();
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
