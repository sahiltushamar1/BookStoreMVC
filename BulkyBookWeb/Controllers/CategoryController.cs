using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        this._db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = this._db.Categories;
        return this.View(objCategoryList);
    }

    public IActionResult Create()
    {
        return this.View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString())
        {
            this.ModelState.AddModelError("NameDisplayOrderSameError", "The Display Order cannot exactly match the Name!");
        }
        if (this.ModelState.IsValid)
        {
            this._db.Categories.Add(obj);
            this._db.SaveChanges();
            this.TempData["success"] = "Category Created Successfully";
            return this.RedirectToAction("Index");
        }
        return this.View(obj);
    }

    public IActionResult Edit(int? id)
    {
        if(id == null || id == 0)
        {
            return this.NotFound();
        }
        Category? categoryFromDb = this._db.Categories.Find(id);
        //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x =>x.Id == id);
        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
        if(categoryFromDb == null)
        {
            return this.NotFound();
        }
        return this.View(categoryFromDb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            this.ModelState.AddModelError("NameDisplayOrderSameError", "The Display Order cannot exactly match the Name!");
        }
        if (this.ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            this.TempData["success"] = "Category Edited Successfully";
            return this.RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return this.NotFound();
        }
        Category? categoryFromDb = _db.Categories.Find(id);
        //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x =>x.Id == id);
        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
        if (categoryFromDb == null)
        {
            return this.NotFound();
        }
        return this.View(categoryFromDb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryFromDb = _db.Categories.Find(id);
        if(categoryFromDb == null)
        {
            return this.NotFound();
        }
        this._db.Categories.Remove(categoryFromDb);
        this._db.SaveChanges();
        this.TempData["success"] = "Category Deleted Successfully";
        return this.RedirectToAction("Index");
        
        
    }
}
