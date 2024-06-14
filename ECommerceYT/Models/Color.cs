using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public Color()
        {
            
        }

        public Color(int colorId, string colorName)
        {
            ColorId = colorId;
            ColorName = colorName;
        }
    }
}