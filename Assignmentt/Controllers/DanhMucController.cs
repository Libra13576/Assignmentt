using Assignmentt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignmentt.Controllers
{
    public class DanhMucController : Controller
    {
        bookstoreEntities2 database = new bookstoreEntities2();
        public PartialViewResult CategoryPartial()
        {
            var cateList = database.Danhmucs.ToList();
            return PartialView(cateList);
        }
        // GET: Categories
        public ActionResult Index(string name)
        {
            if (name == null)
                return View(database.Danhmucs.ToList());
            else
                return View(database.Danhmucs.Where(s => s.tendm.Contains(name)).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Danhmuc category)
        {
            try
            {
                int latestCategoryId = database.Danhmucs.OrderByDescending(c => c.id).Select(c => c.id).FirstOrDefault();
                category.id = latestCategoryId + 1;
                database.Danhmucs.Add(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("LỖI TẠO MỚI CATEGORY");
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Danhmucs.Where(s => s.id == id).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(database.Danhmucs.Where(s => s.id == id).FirstOrDefault());

        }
        [HttpPost]
        public ActionResult Edit(int id, Danhmuc cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Delete(int id)
        {

            return View(database.Danhmucs.Where(s => s.id == id).FirstOrDefault());
        }
        [HttpPost]

        public ActionResult Delete(int id, Danhmuc cate)
        {
            try
            {
                cate = database.Danhmucs.Where(s => s.id == id).FirstOrDefault();
                database.Danhmucs.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");

            }
        }
    }
}