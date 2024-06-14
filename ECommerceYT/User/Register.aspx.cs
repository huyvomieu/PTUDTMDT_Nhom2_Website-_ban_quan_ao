using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Không cần thêm logic vào đây vì chúng ta đã sử dụng IsPostBack để bảo vệ trạng thái của trang
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string postCode = txtPostCode.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Kiểm tra xác nhận mật khẩu
            if (password != confirmPassword)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PasswordMismatch", "alert('Password does not match Confirm Password.');", true);
                return;
            }

            // Tạo kết nối đến cơ sở dữ liệu
            string connectionString = Utils.getConnection(); // Thay thế bằng phương thức kết nối của bạn
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Users (Name, Username, Mobile, Email, Address, PostCode, Password, RoleId, CreatedDate)
                    VALUES (@Name, @Username, @Mobile, @Email, @Address, @PostCode, @Password, @RoleId, @CreatedDate);
                ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@PostCode", postCode);
                cmd.Parameters.AddWithValue("@Password", password); // Cần mã hóa mật khẩu
                cmd.Parameters.AddWithValue("@RoleId", 1); // RoleId = 1 cho vai trò mặc định
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Đăng ký thành công, có thể chuyển hướng người dùng đến trang đăng nhập hoặc thông báo thành công
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // Đăng ký thất bại, hiển thị thông báo lỗi
                        ScriptManager.RegisterStartupScript(this, GetType(), "RegistrationFailed", "alert('Registration failed. Please try again later.');", true);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi thực hiện câu lệnh SQL
                    ScriptManager.RegisterStartupScript(this, GetType(), "DatabaseError", $"alert('Error: {ex.Message}');", true);
                }
            }
        }
    }
}