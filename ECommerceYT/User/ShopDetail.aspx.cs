using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class ShopDetail : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId;
                if (int.TryParse(Request.QueryString["ProductId"], out productId))
                {
                    LoadProductDetails(productId);
                    LoadProductRelated(productId);
                }
                else
                {
                    // Handle error or redirect if ProductId is not provided or invalid
                }
            }
        }
        private void LoadProductDetails(int productId)
        {
            string connectionString = Utils.getConnection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName, Price, Size, Color, Description, ImageUrl, Sold FROM Product WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Set the values to the controls
                    lblProductName.Text = reader["ProductName"].ToString();
                    lblPrice.Text = string.Format("${0:0.00}", reader["Price"]);
                    lblDescription.Text = reader["Description"].ToString();
                    imgProduct.ImageUrl = "../" + reader["ImageUrl"].ToString();
                    lblSold.Text = "Sold: " + reader["Sold"].ToString();
                    hfProductSize.Value = reader["Size"].ToString();
                    hfProductColor.Value = reader["Color"].ToString();
                }
            }
        }

        protected List<string> GetProductSizes()
        {
            // Giả sử bạn có một thuộc tính size trong sản phẩm được lưu dưới dạng một chuỗi ngăn cách bởi dấu phẩy ","
            // Thay thế đoạn code dưới đây bằng logic để lấy danh sách các size từ thuộc tính size của sản phẩm
            string productSizeString = hfProductSize.Value; 
            List<string> sizes = productSizeString.Split(',').ToList();
            return sizes;
        }

        protected List<string> GetProductColors()
        {
            // Giả sử bạn có một thuộc tính size trong sản phẩm được lưu dưới dạng một chuỗi ngăn cách bởi dấu phẩy ","
            // Thay thế đoạn code dưới đây bằng logic để lấy danh sách các size từ thuộc tính size của sản phẩm
            string productSizeString = hfProductColor.Value;
            List<string> sizes = productSizeString.Split(',').ToList();
            return sizes;
        }


        public int GetUserIdByUsername()
        {
            int userId = -1; // Khởi tạo giá trị mặc định

            if(Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            string usernameToSelect = Session["UserName"].ToString();

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT TOP 1 UserId FROM Users WHERE UserName = @UserName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", usernameToSelect);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userId = reader.GetInt32(0); // Lấy giá trị UserId từ kết quả truy vấn
                    }

                    reader.Close();
                }
            }

            return userId;
        }


        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
               

            int userId = GetUserIdByUsername();
            int productId = Convert.ToInt32(Request.QueryString["ProductId"]);
            int quantity = Convert.ToInt32(hfQuantity.Value);

            string size = hfSizeToSave.Value;
            string color = hfColorToSave.Value;

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "INSERT INTO Cart (UserId, ProductId, Quantity, CreatedDate,Size, Color) VALUES (@UserId, @ProductId, @Quantity, GETDATE(), @Size, @Color)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Size", size);
                    cmd.Parameters.AddWithValue("@Color", color);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            lblMsg.Visible = true;
            lblMsg.Text = "them thanh cong san pham so " + productId + " so luong " + quantity + " cua khach hang " + userId + " mau, size: " + color + ", " + size;
            /*Response.Redirect("Cart.aspx");*/
        }

        private void LoadProductRelated(int productIdToGet)
        {
            string query;
            string firstWord = GetFirstWordFromProductName(productIdToGet);

            if (!string.IsNullOrEmpty(firstWord))
            {
                query = @"
            SELECT TOP 5 
                ProductId, 
                ProductName, 
                Price, 
                ImageUrl, 
                Sold,
                NEWID() as RandomOrder
            FROM 
                Product
            WHERE 
                ProductName LIKE @FirstWord + '%'
            UNION
            SELECT TOP 5 
                ProductId, 
                ProductName, 
                Price, 
                ImageUrl, 
                Sold,
                NEWID() as RandomOrder
            FROM 
                Product
            WHERE 
                ProductName NOT LIKE @FirstWord + '%'
            ORDER BY RandomOrder";
            }
            else
            {
                query = @"
            SELECT TOP 5 
                ProductId, 
                ProductName, 
                Price, 
                ImageUrl, 
                Sold
            FROM 
                Product
            ORDER BY NEWID()";
            }

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(firstWord))
                    {
                        cmd.Parameters.AddWithValue("@FirstWord", firstWord);
                    }

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptRelatedProducts.DataSource = dt;
                        rptRelatedProducts.DataBind();
                    }
                }
            }
        }

        private string GetFirstWordFromProductName(int productId)
        {
            string firstWord = string.Empty;
            string query = "SELECT ProductName FROM Product WHERE ProductId = @ProductId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string productName = result.ToString();
                        firstWord = productName.Split(' ')[0];
                    }
                }
            }

            return firstWord;
        }

        protected string ResolveImageUrl(object imageUrl)
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
    }
}