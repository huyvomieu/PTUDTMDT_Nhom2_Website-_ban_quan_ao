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
    public partial class OrderHistoty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderHistory();
            }
        }

        private void BindOrderHistory()
        {
            int userId = GetUserIdFromSession(); // Lấy UserId từ session (hoặc từ đâu đó)

            string query = @"
                SELECT o.Id, o.TotalPrice, o.CreatedTime AS CreatedDate, os.Name AS OrderStatus
                FROM [Order] o
                INNER JOIN OrderStatus os ON o.OrderStatusId = os.Id
                WHERE o.UserId = @UserId
                ORDER BY o.CreatedTime DESC";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            gvOrders.DataSource = dt;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                BindOrderDetails(orderId);
            }
            else if (e.CommandName == "CancelOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                CancelOrder(orderId);
                // Sau khi hủy đơn hàng, cập nhật lại danh sách đơn hàng
                BindOrderHistory();
            }
        }


        private void CancelOrder(int orderId)
        {
            string query = @"
                            UPDATE [Order]
                            SET OrderStatusId = @CancelledStatusId
                            WHERE Id = @OrderId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CancelledStatusId", 5); // Assuming OrderStatusId = 2 represents Cancelled status
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private int GetUserIdFromSession()
        {
            // Implement this method to get UserId from session or authentication system
            int userId = -1; // Khởi tạo giá trị mặc định

            if (Session["UserName"] == null)
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

        private void BindOrderDetails(int orderId)
        {
            string query = @"
                SELECT od.Id, p.ProductName, od.Price AS TotalPrice
                FROM OrderDetail od
                INNER JOIN Product p ON od.ProductId = p.ProductId
                WHERE od.OrderId = @OrderId";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }

            gvOrderDetails.DataSource = dt;
            gvOrderDetails.DataBind();
        }

    }
}