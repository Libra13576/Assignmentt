using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignmentt.Models
{
    public class CreateViewModel
    {
        public Danhmuc Category { get; set; }
        public List<Danhmuc> Categories { get; set; }

        public nhaxuatban Publisher { get; set; }
        public List<nhaxuatban> Publishers { get; set; }
    }
}