using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignmentt.Models
{
    public class CartItem
    {
        public Sach product { get; set; }
        public int quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items { get { return items; } }
        public void Add_Product_Cart(Sach pro, int quan = 1)
        {

            var item = Items.FirstOrDefault(s => s.product.id == pro.id);
            if (item == null)
                items.Add(new CartItem
                {
                    product = pro,
                    quantity = quan
                });
            else item.quantity += quan;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s.quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s.quantity * s.product.gia);
            return (decimal)total;
        }
        public void Update_quantity(int id, int new_quan)
        {
            var item = items.Find(s => s.product.id == id);
            if (item != null)
            {
                if (items.Find(s => s.product.soluong < 0) != null)
                    item.quantity = 0;
                else
                {
                    if (items.Find(s => s.product.soluong >= new_quan) != null) // nếu số lượng mua nhỏ hơn số lượng tồn
                        item.quantity = new_quan; //thì chấp nhận số lượng mua
                    else item.quantity = 1; //ngược lại, thì số lượng mua trả về 1
                }
            }
        }
        public void Remove_cartitem(int id)
        {
            items.RemoveAll(s => s.product.id == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }

    }
}