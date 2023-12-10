using Assignmentt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignmentt.Controllers
{
    public class ShoppingCartController : Controller
    {
        bookstoreEntities2 database = new bookstoreEntities2();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = database.Saches.SingleOrDefault(s => s.id == id);
            if (pro != null)
            {
                GetCart().Add_Product_Cart(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult update_cart_quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idpro = int.Parse(form["idPro"]);
            int quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(idpro, quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_cartitem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity = cart.Total_quantity();
            ViewBag.QuantityCart = total_quantity;
            return PartialView("BagCart");
        }
        public ActionResult CheckOut()
        {
            var lastUserId = database.donhangs.OrderByDescending(u => u.id).Select(u => u.id).FirstOrDefault();
            var lastUserId2 = database.chitietdonhangs.OrderByDescending(u => u.id).Select(u => u.id).FirstOrDefault();

            int id = int.Parse(Session["ID"].ToString());
            var pro = database.khachhangs.SingleOrDefault(s => s.id == id);
            Cart cart = Session["Cart"] as Cart;
            donhang order = new donhang();
            order.id = lastUserId + 1; ;
            order.ngaydh = DateTime.Now;
            order.idkh = id;
            order.hoten = pro.hoten;
            order.sdt = pro.sdt;
            order.tonggia = 0;
            order.diachi = pro.diachi;
            database.donhangs.Add(order);
            database.SaveChanges();
            foreach (var item in cart.Items)
            {
                chitietdonhang other_detail = new chitietdonhang();
                other_detail.id = lastUserId2 + 1;
                other_detail.iddh = order.id;
                other_detail.tensach = item.product.tensach;
                other_detail.soluong = item.quantity;
                other_detail.hinhanh = item.product.hinhanh;
                other_detail.gia = (decimal)item.product.gia;
                database.chitietdonhangs.Add(other_detail);
                database.SaveChanges();
                foreach (var p in database.Saches.Where(s => s.id == other_detail.id))
                {
                    var update_quan_pro = p.soluong - item.quantity;
                    p.soluong = update_quan_pro;
                }
            }
            database.SaveChanges();
            cart.ClearCart();
            return RedirectToAction("Index", "Sach");
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
    }
}