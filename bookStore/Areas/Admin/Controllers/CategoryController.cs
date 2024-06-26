using bookStore.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;
        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.CategoryRepository.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Upsert(int?id)
        {
            Category c = new Category();
            if (id == null || id == 0)
            {
                //create
                return View(c);
            }
            else
            {
                //update
                Category? categoryID = _db.CategoryRepository.Get(u => u.Ctg_ID == id);
                return View(categoryID);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Category c)
        {
            if (ModelState.IsValid)
            {
                if (c.Ctg_ID == 0)
                {
                    _db.CategoryRepository.Add(c);
                }
                else
                {
                    _db.CategoryRepository.Update(c);
                }

                _db.save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(c);
            }
        }

      

      

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoryList = _db.CategoryRepository.GetAll().ToList();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CategoryToBeDeleted = _db.CategoryRepository.Get(u => u.Ctg_ID == id);
            if (CategoryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            
            _db.CategoryRepository.Remove(CategoryToBeDeleted);
            _db.save();

            return Json(new { success = true, message = "Delete Successfull" });
        }


        #endregion

    }

}
