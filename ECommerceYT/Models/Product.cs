using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        public int Sold { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }

        public Product()
        {
            
        }

        public Product(int productId, string productName, string description, decimal price, int quantity, string size, string color, int categoryId, int sold, bool isActive, DateTime createdDate, string imageUrl)
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Size = size;
            Color = color;
            CategoryId = categoryId;
            Sold = sold;
            IsActive = isActive;
            CreatedDate = createdDate;
            ImageUrl = imageUrl;
        }
    }
}