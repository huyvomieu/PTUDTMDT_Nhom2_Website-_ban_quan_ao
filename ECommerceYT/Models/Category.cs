using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class Category
    {
        public Int32 CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Category()
        {
            
        }

        public Category(int categoryId, string categoryName, string categoryImageUrl, bool isActive)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryImageUrl = categoryImageUrl;
            IsActive = isActive;
        }
    }
}