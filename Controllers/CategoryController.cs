using Bookie.Data;
using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitOfwork)
        {
            _unitofwork= unitOfwork;
        }

        public IActionResult Index()
        {   
            IEnumerable<Category> ObjCategoryList = _unitofwork.Category.GetAll();
            return View(ObjCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		//GET
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//var categoryFromDb = _Db.Categories.Find(id);
			var categoryfromdbfirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (categoryfromdbfirst == null)
			{
				return NotFound();
			}

			return View(categoryfromdbfirst);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				_unitofwork.Category.Update(obj);
				_unitofwork.Save();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//GET
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//var categoryFromDb = _Db.Categories.Find(id);
			var categoryFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}

			return View(categoryFromDbFirst);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int? id)
		{
			var obj = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
			{
				return NotFound();
			}

			_unitofwork.Category.Remove(obj);
			_unitofwork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
