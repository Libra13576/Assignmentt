using Assignmentt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Assignmentt.Controllers
{
    public class SachController : Controller
    {
        bookstoreEntities2 database = new bookstoreEntities2();
        public ActionResult Index(int? category, string tensp, int? page, double min = double.MinValue, double max = double.MaxValue)
        {
            int pageSize = 5;
            int pageNum = page ?? 1;

            var productList = database.Saches.AsQueryable(); // Start with all products

            if (!string.IsNullOrEmpty(tensp))
            {
                productList = productList.Where(s => s.tensach.Contains(tensp));
            }

            if (min != double.MinValue || max != double.MaxValue)
            {
                productList = productList.Where(p => (double)p.gia >= min && (double)p.gia <= max);
            }

            if (category.HasValue)
            {
                productList = productList.Where(s => s.iddm == category);
            }

            productList = productList.OrderByDescending(x => x.tensach);

            return View(productList.ToPagedList(pageNum, pageSize));
        }
        public ActionResult spadmin()
        {
            return View(database.Saches.ToList());
        }
        public ActionResult Create()
        {
            List<Danhmuc> list = database.Danhmucs.ToList();
            ViewBag.ListCategory = new SelectList(list, "id", "tendm");
            List<nhaxuatban> list2 = database.nhaxuatbans.ToList();
            ViewBag.Listnhaxb = new SelectList(list2, "id", "tennxb");
            Sach pro = new Sach();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Sach product)
        {
            var lastUserId = database.Saches.OrderByDescending(u => u.id).Select(u => u.id).FirstOrDefault();
            product.id = lastUserId + 1;
            List<Danhmuc> list = database.Danhmucs.ToList();
            List<nhaxuatban> list2 = database.nhaxuatbans.ToList();
            try
            {
                if (product.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(product.UploadImage.FileName);
                    String exten = Path.GetExtension(product.UploadImage.FileName);
                    filename = filename + exten;
                    product.hinhanh = "~/Content/images/" + filename;
                    product.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCategory = new SelectList(list, "id", "tendm", 1);
                ViewBag.Listnhaxb = new SelectList(list2, "id", "tennxb", 1);
                database.Saches.Add(product);
                database.SaveChanges();
                return RedirectToAction("spadmin");
            }
            catch { return View(); }
        }

        public ActionResult SelectCate()
        {
            Danhmuc se_cate = new Danhmuc();
            se_cate.ListCate = database.Danhmucs.ToList<Danhmuc>();
            return PartialView(se_cate);
        }
        public ActionResult SelectCate2()
        {
            nhaxuatban se_cate = new nhaxuatban();
            se_cate.ListCate = database.nhaxuatbans.ToList<nhaxuatban>();
            return PartialView(se_cate);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sach pro = database.Saches.FirstOrDefault(p => p.id == id);
            if (pro != null)
                return View(pro);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sach product, HttpPostedFileBase ImagePro)
        {
            if (ModelState.IsValid)
            {
                var productDB = database.Saches.FirstOrDefault(p => p.id == product.id);
                if (productDB != null)
                {
                    productDB.tensach = product.tensach;
                    productDB.thongtinsach = product.thongtinsach;
                    productDB.gia = product.gia;
                    productDB.soluong = product.soluong;
                    if (ImagePro != null)
                    {
                        //Lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(ImagePro.FileName);
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                        //Lưu tên
                        productDB.hinhanh = fileName;
                        //Save vào Images Folder
                        ImagePro.SaveAs(path);
                    }
                    //productDB.Category = product.Category;
                }
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(database.Danhmucs, "id", "tendm", product.Danhmuc);
            ViewBag.Category = new SelectList(database.nhaxuatbans, "id", "tennxb", product.Danhmuc);
            return View(product);

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Sach pro = database.Saches.FirstOrDefault(p => p.id == id);
            if (pro != null)
                return View(pro);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var productDB = database.Saches.FirstOrDefault(p => p.id == 1);
            if (productDB != null)
            {
                database.Saches.Remove(productDB); database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(database.Saches.Where(s => s.id == id).FirstOrDefault());
        }
        public ActionResult Hangtonkho()
        {
            return View(database.Saches.Where(s => s.soluong > 0));
        }
        public ActionResult Hangcannhap()
        {
            return View(database.Saches.Where(s => s.soluong == 0));
        }
    }
}