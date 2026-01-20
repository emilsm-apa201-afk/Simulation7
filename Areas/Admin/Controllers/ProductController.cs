using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation7.Areas.Admin.ViewModels.Product;
using Simulation7.DAL;
using Simulation7.Models;
using Simulation7.Utilities.Enums;
using Simulation7.Utilities.Extensions;

namespace Simulation7.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createProductVm.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "Select correct image format!");
                return View(createProductVm);
            }
            if (createProductVm.ImageFile.ValidateSize(FileSize.GB, 2))
            {
                ModelState.AddModelError("ImageFile", "Size msut be less than 100 kb");
                return View(createProductVm);
            }

            Product product = new()
            {
                Image = await createProductVm.ImageFile.CreateFileAsync(_env.WebRootPath, "assets", "images"),
                Name = createProductVm.Name,
                Price = createProductVm.Price,
                Description = createProductVm.Description,

            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            UpdateProductVm updateProductVm = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };
            return View(updateProductVm);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int? id, UpdateProductVm updateProductVm)
        {

            Product? product = await _context.Products.FirstOrDefaultAsync(w => w.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if (await _context.Products.AnyAsync(w => w.Name == updateProductVm.Name && w.Id != id))
            {
                ModelState.AddModelError("Fullname", "This worker fullname is already taken by another worker.");
                return View(updateProductVm);
            }

            product.Name = updateProductVm.Name;

            if (updateProductVm.ImageFile != null)
            {
                if (!updateProductVm.ImageFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("ImageFile", "Select correct image format!");
                    return View(updateProductVm);
                }
                if (updateProductVm.ImageFile.ValidateSize(FileSize.GB, 2))
                {
                    ModelState.AddModelError("ImageFile", "Size msut be less than 100 kb");
                    return View(updateProductVm);
                }

            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(w => w.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
