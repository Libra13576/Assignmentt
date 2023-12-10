using Assignmentt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignmentt.Controllers
{
    public class OrderProController : Controller
    {
        bookstoreEntities2 database = new bookstoreEntities2();
        // GET: OrderPro
        public ActionResult Index()
        {
            return View(database.donhangs.ToList());
        }
        public ActionResult dsdh()
        {
            int id = int.Parse(Session["ID"].ToString());
            return View(database.donhangs.Where(s => s.idkh == id).ToList());
        }
    }
}