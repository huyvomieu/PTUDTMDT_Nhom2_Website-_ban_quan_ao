using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class ShopWith : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string categoryName = Request.QueryString["CategoryName"]; // Lấy CategoryName từ query string

                if (!string.IsNullOrEmpty(categoryName))
                {
                    BindProductsByCategory(categoryName);
                }
            }
        }

        public string ResolveImageUrl(object imageUrl)
        {
            return ECommerceYT.Utils.GetImageUrl(imageUrl);
        }

        public string GetTimeAgo(object createdDate)
        {
            DateTime dt = (DateTime)createdDate;
            TimeSpan span = DateTime.Now.Subtract(dt);

            if (span.Days > 365)
            {
                int years = span.Days / 365;
                return $"{years} year{(years > 1 ? "s" : "")} ago";
            }
            if (span.Days > 30)
            {
                int months = span.Days / 30;
                return $"{months} month{(months > 1 ? "s" : "")} ago";
            }
            if (span.Days > 0)
            {
                return $"{span.Days} day{(span.Days > 1 ? "s" : "")} ago";
            }
            if (span.Hours > 0)
            {
                return $"{span.Hours} hour{(span.Hours > 1 ? "s" : "")} ago";
            }
            if (span.Minutes > 0)
            {
                return $"{span.Minutes} minute{(span.Minutes > 1 ? "s" : "")} ago";
            }
            return "Just now";
        }

        private void BindProductsByCategory(string categoryName)
        {
            string query = @"
                    SELECT p.ProductId, p.ProductName, p.Price, p.Sold, p.CreatedDate, p.ImageUrl, c.CategoryName
                    FROM Product p
                    INNER JOIN Category c ON p.CategoryId = c.CategoryId
                    WHERE c.CategoryName = @CategoryName
                    ORDER BY p.CreatedDate DESC";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            rptProducts.DataSource = dt;
            rptProducts.DataBind();
        }

        
    }
}