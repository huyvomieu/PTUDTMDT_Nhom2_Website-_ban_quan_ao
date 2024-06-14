using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!LoginCheck())
            {
                // Nếu chưa đăng nhập, chuyển hướng tới trang đăng nhập
                Response.Redirect("~/User/Login.aspx");
            }
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private bool LoginCheck()
        {
            if(Session["UserName"] != null)
            {
                return true;
            }
            return false;
        }

        private void BindCart()
        {
            var userId = GetUserIdFromSession();

            string query = @"
                    SELECT 
                    c.ProductId, 
                    c.Quantity, 
                    c.Size, 
                    c.Color, 
                    p.ProductName, 
                    p.ImageUrl, 
                    p.Price 
                FROM 
                    Cart c 
                    INNER JOIN Product p ON c.ProductId = p.ProductId 
                WHERE 
                    c.UserId = @UserId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptCart.DataSource = dt;
                        rptCart.DataBind();

                        // Tính toán Subtotal
                        decimal subtotal = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            subtotal += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
                        }

                        // Cập nhật các thẻ Subtotal và Total
                        lblSubtotal.Text = "$" + subtotal.ToString("0.00");
                        lblTotal.Text = "$" + (subtotal + 10).ToString("0.00"); // Shipping fixed at $10
                    }
                }
            }
        }

        protected void rptCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");

                if (txtQuantity != null)
                {
                    txtQuantity.Text = row["Quantity"].ToString();
                }

                decimal price = Convert.ToDecimal(row["Price"]);
                int quantity = Convert.ToInt32(row["Quantity"]);
                lblTotal.Text = (price * quantity).ToString();
            }
        }

        private int GetUserIdFromSession()
        {
            // Implement this method to get UserId from session or authentication system
            int userId = -1; // Khởi tạo giá trị mặc định

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

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productId = Convert.ToInt32(btn.CommandArgument);

            // Xóa mục khỏi cơ sở dữ liệu
            RemoveItemFromCart(productId);

            // Tải lại dữ liệu để cập nhật hiển thị
            BindCart();
        }

        private void RemoveItemFromCart(int productId)
        {
            // Thực hiện mã xóa mục khỏi cơ sở dữ liệu dựa trên productId
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "DELETE FROM Cart WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            int index = item.ItemIndex;

            // Update the quantity in your data source
            // Example:
            // YourDataSource.Rows[index]["Quantity"] = Convert.ToInt32(txtQuantity.Text);
            // YourDataSource.AcceptChanges();

            // Rebind the repeater to reflect the changes
            BindCart();
        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            Button btnMinus = (Button)sender;
            int index = Convert.ToInt32(btnMinus.CommandArgument);
            UpdateQuantity(index, -1);
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
            Button btnPlus = (Button)sender;
            int index = Convert.ToInt32(btnPlus.CommandArgument);
            UpdateQuantity(index, 1);
        }

        private void UpdateQuantity(int index, int delta)
        {
            // Lấy ProductId từ Repeater
            RepeaterItem item = rptCart.Items[index];
            HiddenField hfProductId = (HiddenField)item.FindControl("hfProductId");
            int productId = Convert.ToInt32(hfProductId.Value);

            // Lấy UserId từ session hoặc phương thức bạn đã định nghĩa
            int userId = GetUserIdFromSession();

            // Truy vấn SQL để cập nhật số lượng
            string query = @"
                            UPDATE Cart 
                            SET Quantity = CASE 
                                WHEN Quantity + @Delta < 1 THEN 1 
                                ELSE Quantity + @Delta 
                            END 
                            WHERE ProductId = @ProductId AND UserId = @UserId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Delta", delta);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Gọi lại BindCart để cập nhật giao diện
            BindCart();
        }

    }

}