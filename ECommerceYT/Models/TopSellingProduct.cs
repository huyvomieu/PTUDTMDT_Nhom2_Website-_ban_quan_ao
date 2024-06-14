using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class TopSellingProduct
    {
        public int ProductId { get; set; }
        public int Sold { get; set; }
        public decimal TotalMoneyEarned { get; set; }
        public int SoldThisWeek { get; set; }
        public int SoldThisMonth { get; set; }
        public int SoldThisYear { get; set; }
    }

}