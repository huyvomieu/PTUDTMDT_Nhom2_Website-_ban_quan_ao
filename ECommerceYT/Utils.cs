using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using ECommerceYT.Models;
using Org.BouncyCastle.Tls;

namespace ECommerceYT
{
    public class Utils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataTable dt;

        //get connection string from "cs"
        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        //check the file type isValie
        public static bool isValidExtension(string filename)
        {
            bool isValie = false;
            string[] fileExtension = { ".jpg", ".png", ".jpeg" };
            foreach (var ext in fileExtension)
            {
                if(filename.Contains(ext))
                {
                    isValie = true;
                    break;
                }
            }
            return isValie;
        }

        public static string getUniqueId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static string GetImageUrl(Object url)
        {
            string url1 = string.Empty;
            if (url == null || url == DBNull.Value || string.IsNullOrEmpty(url.ToString()))
            {
                url1 = "../Images/No_image.png";
            }
            else
            {
                url1 = string.Format("../{0}", url);
            }
            return url1;
        }

        public static string GetIsActiveText(object isActive)
        {
            if (isActive == null || isActive == DBNull.Value)
            {
                return "In-Active";
            }

            return (bool)isActive ? "Active" : "In-Active";
        }

        public static string GetIsActiveCssClass(object isActive)
        {
            if (isActive == null || isActive == DBNull.Value)
            {
                return "badge badge-danger";
            }

            return (bool)isActive ? "badge badge-success" : "badge badge-danger";
        }

        public static List<Role> GetAllRole()
        {
            List<Role> roles = new List<Role>();
            string sql = "select * from Roles";

            using (SqlConnection con = new SqlConnection(getConnection()))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Role role = new Role
                        {
                            RoleId = (int)rd["RoleId"],
                            RoleName = (string)rd["RoleName"]
                        };
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        public static List<Category> GetAllCategory()
        {
            List<Category> cates = new List<Category>();
            string sql = "select * from Category";

            using (SqlConnection con = new SqlConnection(getConnection()))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Category category = new Category
                        {
                            CategoryId = (int)rd["CategoryId"],
                            CategoryName = (string)rd["CategoryName"],
                            CategoryImageUrl = (string)rd["CategoryImageUrl"]
                        };
                        cates.Add(category);
                    }
                }
            }
            return cates;
        }

        public static List<Product> GetProductsSortedBySold()
        {
            List<Product> products = new List<Product>();
            string sql = "SELECT * FROM Product ORDER BY Sold DESC";

            using (SqlConnection con = new SqlConnection(getConnection()))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)rd["ProductId"],
                            ProductName = (string)rd["ProductName"],
                            Description = (string)rd["Description"],
                            Price = (decimal)rd["Price"],
                            Quantity = (int)rd["Quantity"],
                            Size = (string)rd["Size"],
                            Color = (string)rd["Color"],
                            CategoryId = (int)rd["CategoryId"],
                            Sold = (int)rd["Sold"],
                            IsActive = (bool)rd["IsActive"],
                            CreatedDate = (DateTime)rd["CreatedDate"],
                            ImageUrl = (string)rd["ImageUrl"]
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public static List<Product> GetProductsSortedByCreatedDate()
        {
            List<Product> products = new List<Product>();
            string sql = "SELECT * FROM Product ORDER BY CreatedDate DESC";

            using (SqlConnection con = new SqlConnection(getConnection()))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)rd["ProductId"],
                            ProductName = (string)rd["ProductName"],
                            Description = (string)rd["Description"],
                            Price = (decimal)rd["Price"],
                            Quantity = (int)rd["Quantity"],
                            Size = (string)rd["Size"],
                            Color = (string)rd["Color"],
                            CategoryId = (int)rd["CategoryId"],
                            Sold = (int)rd["Sold"],
                            IsActive = (bool)rd["IsActive"],
                            CreatedDate = (DateTime)rd["CreatedDate"],
                            ImageUrl = (string)rd["ImageUrl"]
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string sql = "SELECT * FROM Product";

            using (SqlConnection con = new SqlConnection(getConnection()))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)rd["ProductId"],
                            ProductName = (string)rd["ProductName"],
                            Description = (string)rd["Description"],
                            Price = (decimal)rd["Price"],
                            Quantity = (int)rd["Quantity"],
                            Size = (string)rd["Size"],
                            Color = (string)rd["Color"],
                            CategoryId = (int)rd["CategoryId"],
                            Sold = (int)rd["Sold"],
                            IsActive = (bool)rd["IsActive"],
                            CreatedDate = (DateTime)rd["CreatedDate"],
                            ImageUrl = (string)rd["ImageUrl"]
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public static Models.User GetUserInformation(int userId)
        {
            Models.User user = null;
            string query = @"
                            SELECT [UserId], [Name], [Username], [Mobile], [Email], [Address], [PostCode], [Password], [ImageUrl], [RoleId], [CreatedDate]
                            FROM [Users]
                            WHERE [UserId] = @UserId";

            // Thực hiện kết nối và truy vấn cơ sở dữ liệu
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new Models.User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Username = reader["Username"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            PostCode = reader["PostCode"].ToString(),
                            Password = reader["Password"].ToString(), // Lưu ý: không nên lấy mật khẩu nhưng để minh họa
                            ImageUrl = reader["ImageUrl"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };
                    }
                    reader.Close();
                }
            }

            return user;
        }

        public static void CheckLoginStatus()
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                // Chuyển hướng về trang đăng nhập nếu người dùng chưa đăng nhập
                HttpContext.Current.Response.Redirect("~/User/Login.aspx");
            }
            
        }

        public static int GetUserIdFromSession(string usernameToSelect)
        {
            // Implement this method to get UserId from session or authentication system
            int userId = -1; // Khởi tạo giá trị mặc định

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
    }
}