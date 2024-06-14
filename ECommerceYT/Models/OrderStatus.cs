using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constructor
        public OrderStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}