using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class UserInfor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                int userId;
                if (int.TryParse(Request.QueryString["UserId"], out userId))
                {
                    LoadUserInformation(userId);
                }
                else
                {
                    lblMessage.Text = "Invalid User ID.";
                }
            }
        }

        private void LoadUserInformation(int userId)
        {
            Models.User user = Utils.GetUserInformation(userId);
            if (user != null)
            {
                txtUserId.Text = user.UserId.ToString();
                txtName.Text = user.Name;
                txtUsername.Text = user.Username;
                txtMobile.Text = user.Mobile;
                txtEmail.Text = user.Email;
                txtAddress.Text = user.Address;
                txtPostCode.Text = user.PostCode;
                txtImageUrl.Text = user.ImageUrl;
            }
            else
            {
                lblMessage.Text = "User not found.";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Models.User user = Utils.GetUserInformation(int.Parse(txtUserId.Text));

            if (user == null)
            {
                lblMessage.Text = "User not found.";
                return;
            }

            // Kiểm tra mật khẩu cũ
            if (!string.IsNullOrEmpty(txtOldPassword.Text))
            {
                if (user.Password != txtOldPassword.Text)
                {
                    lblMessage.Text = "Old password is incorrect.";
                    return;
                }

                // Kiểm tra mật khẩu mới và xác nhận
                if (txtNewPassword.Text != txtReEnterNewPassword.Text)
                {
                    lblMessage.Text = "New passwords do not match.";
                    return;
                }

                user.Password = txtNewPassword.Text; // Cập nhật mật khẩu mới
            }

            user.Name = txtName.Text;
            user.Username = txtUsername.Text;
            user.Mobile = txtMobile.Text;
            user.Email = txtEmail.Text;
            user.Address = txtAddress.Text;
            user.PostCode = txtPostCode.Text;
            user.ImageUrl = txtImageUrl.Text;
            

            bool result = UpdateUserInformation(user);
            if (result)
            {
                lblMessage.Text = "User updated successfully.";
            }
            else
            {
                lblMessage.Text = "Error updating user.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx"); // Thay thế bằng trang danh sách người dùng của bạn
        }

        private bool UpdateUserInformation(Models.User user)
        {
            string query = @"
                    UPDATE [Users]
                    SET [Name] = @Name,
                        [Username] = @Username,
                        [Mobile] = @Mobile,
                        [Email] = @Email,
                        [Address] = @Address,
                        [PostCode] = @PostCode,
                        [ImageUrl] = @ImageUrl,
                        [Password] = @Password,
                    WHERE [UserId] = @UserId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@PostCode", user.PostCode);
                    cmd.Parameters.AddWithValue("@ImageUrl", user.ImageUrl);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}