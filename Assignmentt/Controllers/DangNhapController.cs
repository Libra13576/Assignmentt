using Assignmentt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignmentt.Controllers
{
    public class DangNhapController : Controller
    {
        bookstoreEntities2 database = new bookstoreEntities2();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult thongtintk()
        {
            int id = int.Parse(Session["ID"].ToString());
            return View(database.khachhangs.Where(s => s.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult LoginAcount(khachhang user)
        {
            var check = database.khachhangs.Where(s => s.hoten == user.hoten && s.matkhau == user.matkhau).FirstOrDefault();

            if (check == null)
            {
                ViewBag.ErrorInfo = "sai info";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = check.id;
                Session["Name"] = check.hoten;

                return RedirectToAction("Index", "Sach");
                //return View("Index");
            }
        }
        public ActionResult dangnhapadmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangnhapadmin(Quanlycuahang user)
        {
            var tk = "Adminshoppee";
            var mk = "123456789";
            if (tk == user.hoten && mk == user.matkhau)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("dangnhapadmin");
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(khachhang user)
        {
            if (ModelState.IsValid)
            {
                var lastUserId = database.khachhangs.OrderByDescending(u => u.id).Select(u => u.id).FirstOrDefault();
                user.id = lastUserId + 1;
                var check = database.khachhangs.Where(s => s.hoten == user.hoten).FirstOrDefault();
                if (check == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.khachhangs.Add(user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "this ID is Exixst";
                    return View();
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("Index", "DangNhap");
        }
    }
}