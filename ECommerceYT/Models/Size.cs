using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class Size
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }

        public Size()
        {
            
        }

        public Size(int sizeId, string sizeName)
        {
            SizeId = sizeId;
            SizeName = sizeName;
        }
    }
}