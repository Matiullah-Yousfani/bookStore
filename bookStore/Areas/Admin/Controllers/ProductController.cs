using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> productList = _db.ProductRepository.GetAll(includeProperties:"Category").ToList();
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new ProductVM()
            {
                CategoryList = _db.CategoryRepository.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Ctg_ID.ToString()
                }
                ).ToList(),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _db.ProductRepository.Get(u => u.ProdID == id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM p,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/product");
                    if (!string.IsNullOrEmpty(p.Product.ImageURL))
                    {
                        //delete the old image 
                        var oldimagepath = Path.Combine(wwwRootPath, p.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }

                    }

                    using (var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream); 
                    }
                    p.Product.ImageURL = @"\images\product\" + filename;
                }
                if (p.Product.ProdID == 0)
                {
                    _db.ProductRepository.Add(p.Product);
                }
                else
                {
                    _db.ProductRepository.Update(p.Product);
                }
                
                _db.save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                p.CategoryList = _db.CategoryRepository.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Ctg_ID.ToString()
                }
                ).ToList();
                return View(p);
            }
            
        }

        

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> productList = _db.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _db.ProductRepository.Get(u=>u.ProdID == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldimagepath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldimagepath))
            {
                System.IO.File.Delete(oldimagepath);
            }
            _db.ProductRepository.Remove(productToBeDeleted);
            _db.save();

            return Json(new { success = true, message = "Delete Successfull" });
        }


        #endregion
    }

}
