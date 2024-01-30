using Microsoft.AspNetCore.Mvc;
using DERS50_NORTWİND_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DERS50_NORTWİND_CRUD.Controllers
{
    public class ProductController : Controller
    {
        NortwindContext context= new NortwindContext();

        public async Task<IActionResult> Index()
        {

            return View( await context.Products.OrderByDescending(p=>p.ProductID).ToListAsync());

        }

        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult>  Create([Bind("ProductName,UnitPrice")] Product product) 
        {
            if (ModelState.IsValid) 
            {
                context.Add(product);
                //context.SaveChanges();
                await context.SaveChangesAsync(); 
                
            }
            
            return View(product);
            // return View(); //Create.cshtml
            // return RedirectToAction("Index"); 
            //return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null|| context.Products==null) 
            {
                return NotFound();
            }

            var product= await context.Products.FindAsync(id);

            if (product==null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(int id,[Bind("ProductName,UnitPrice,ProductID")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    context.Update(product);
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (ProductExists(product.ProductID)) 
                    {
                        return NotFound();
                    }
                    throw;
                }
                context.Add(product);
               
            }

            return View(product);
        }

        private bool ProductExists(int id) 
        {
        return context.Products.Any(p=> p.ProductID == id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(p=>p.ProductID==id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost,ActionName("Delete")] 
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Products==null)
            {
                return NotFound();
            }
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id==null || context.Products==null)
            {
                return NotFound();
            }

            var product= await context.Products.FirstOrDefaultAsync(p=> p.ProductID==id);
            if (product == null) 
            {
                return NotFound();
            }
            return View(product);
        }


    }
}
