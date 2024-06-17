using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECommerceYT.Models;

namespace ECommerceYT.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderData();
                LoadOrderStatusDropDown();
            }
        }

        private void BindOrderData()
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT o.Id, o.TotalPrice, o.UserId, p.PaymentMethod, os.Name as OrderStatusName, o.CreatedTime
                    FROM [Order] o
                    INNER JOIN Payment p ON o.PaymentId = p.PaymentId
                    INNER JOIN OrderStatus os ON o.OrderStatusId = os.Id
            ", con))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();

                    rProduct.DataSource = dt;
                    rProduct.DataBind();
                }
            }
        }

        private void LoadOrderStatusDropDown()
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM OrderStatus", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ddlOrderStatusEdit.DataSource = rdr;
                    ddlOrderStatusEdit.DataTextField = "Name";
                    ddlOrderStatusEdit.DataValueField = "Id";
                    ddlOrderStatusEdit.DataBind();
                    con.Close();
                }
            }
        }

        protected void btnUpdateOrderStatus_Click(object sender, EventArgs e)
        {
            // Xử lý cập nhật OrderStatus khi người dùng nhấn nút Update
            // Lấy Id của đơn hàng được chọn từ Label lblSelectedOrderId
            int orderId = Convert.ToInt32(lblSelectedOrderId.Text);

            // Lấy giá trị OrderStatus mới từ DropDownList ddlOrderStatusEdit
            int newOrderStatusId = Convert.ToInt32(ddlOrderStatusEdit.SelectedValue);

            // Thực hiện câu lệnh cập nhật trong database
            UpdateOrderStatus(orderId, newOrderStatusId);

            // Sau khi cập nhật, cần reload dữ liệu đơn hàng
            BindOrderData();
        }


        private void UpdateOrderStatus(int orderId, int newOrderStatusId)
        {
            string query = "UPDATE [Order] SET OrderStatusId = @NewOrderStatusId WHERE Id = @OrderId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NewOrderStatusId", newOrderStatusId);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "edit")
            {
                lblSelectedOrderId.Text = e.CommandArgument.ToString();
                btnUpdateOrderStatus.Enabled = true;
            }
        }
    }
}