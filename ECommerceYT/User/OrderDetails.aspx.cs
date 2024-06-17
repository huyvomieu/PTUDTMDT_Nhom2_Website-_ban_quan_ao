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
    public partial class OrderDetails : System.Web.UI.Page
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
                if (Request.QueryString["OrderId"] != null)
                {
                    int orderId = Convert.ToInt32(Request.QueryString["OrderId"]);
                    BindOrderDetails(orderId);
                }

            }
        }

        private bool LoginCheck()
        {
            if (Session["UserName"] != null)
            {
                return true;
            }
            return false;
        }

        private void BindOrderDetails(int orderId)
        {
            string query = @"
                SELECT o.OrderId, o.TotalPrice, os.OrderStatus, u.Name, u.Email, u.Mobile,
                       od.ProductId, p.ProductName, od.Quantity, od.Price, od.TotalPrice AS ItemTotalPrice
                FROM [Order] o
                INNER JOIN OrderStatus os ON o.Id = os.Id
                INNER JOIN Users u ON o.UserId = u.UserId
                INNER JOIN OrderDetail od ON o.Id = od.Id
                INNER JOIN Product p ON od.ProductId = p.ProductId
                WHERE o.Id = @OrderId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];

                            lblOrderId.InnerText = row["OrderId"].ToString();
                            lblTotalPrice.InnerText = string.Format("{0:C}", row["TotalPrice"]);
                            lblOrderStatus.InnerText = row["OrderStatus"].ToString();
                            lblCustomerName.InnerText = row["Name"].ToString();
                            lblCustomerEmail.InnerText = row["Email"].ToString();
                            lblCustomerMobile.InnerText = row["Mobile"].ToString();

                            gvOrderItems.DataSource = dt;
                            gvOrderItems.DataBind();
                        }
                        else
                        {
                            // Handle case when no order details are found
                        }
                    }
                }
            }
        }
    }
}