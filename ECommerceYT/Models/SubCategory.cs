using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceYT.Models
{
    public class SubCategory
    {
        public Int32 SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Int32 CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Cateateddate { get; set; } = DateTime.Now;

        public SubCategory()
        {
            
        }

        public SubCategory(int subCategoryId, string subCategoryName, int categoryName, bool isActive, DateTime cateateddate)
        {
            SubCategoryId = subCategoryId;
            SubCategoryName = subCategoryName;
            CategoryName = categoryName;
            IsActive = isActive;
            Cateateddate = cateateddate;
        }
    }
}