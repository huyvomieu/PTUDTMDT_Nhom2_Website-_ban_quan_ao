using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string name, imageUrl;
            int roleId;

            if (AuthenticateUser(username, password, out name, out imageUrl, out roleId))
            {
                // Lưu thông tin người dùng vào session và chuyển hướng đến trang chủ
                Session["UserNameAdmin"] = username;
                Session["NameAdmin"] = name;
                Session["ImageUrlAdmin"] = imageUrl;
                Session["RoleId"] = roleId; 
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool AuthenticateUser(string username, string password, out string name, out string imageUrl, out int roleId)
        {
            bool isAuthenticated = false;
            name = string.Empty;
            imageUrl = string.Empty;
            roleId = -1;
            

            string connectionString = Utils.getConnection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, ImageUrl, RoleId FROM Users WHERE UserName=@UserName AND Password=@Password AND RoleId=1";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader["Name"].ToString();
                        imageUrl = reader["ImageUrl"].ToString();
                        roleId = Convert.ToInt32(reader["RoleId"]);
                        isAuthenticated = true;
                    }
                }
            }
            return isAuthenticated;
        }


    }
}