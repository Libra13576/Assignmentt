//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignmentt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Sach
    {
        public Sach()
        {
            hinhanh = "~/Content/images/1.png";
        }

        public int id { get; set; }
        public string tensach { get; set; }
        public Nullable<int> iddm { get; set; }
        public string thongtinsach { get; set; }
        public decimal gia { get; set; }
        public int soluong { get; set; }
        public Nullable<int> idnxb { get; set; }
        public string hinhanh { get; set; }
    
        public virtual Danhmuc Danhmuc { get; set; }
        public virtual nhaxuatban nhaxuatban { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
    }
}
