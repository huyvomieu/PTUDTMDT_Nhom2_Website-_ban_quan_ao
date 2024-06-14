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

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (drv != null)
                {
                    DropDownList ddlOrderStatus = e.Item.FindControl("ddlOrderStatus") as DropDownList;
                    if (ddlOrderStatus != null)
                    {
                        // Bind dropdownlist with order status from database
                        ddlOrderStatus.DataSource = GetOrderStatusList(); // Replace with your method to get order status list
                        ddlOrderStatus.DataTextField = "Name"; // Change this to your actual field name for display
                        ddlOrderStatus.DataValueField = "Id"; // Change this to your actual field name for value
                        ddlOrderStatus.DataBind();

                        // Set selected value based on current order status
                        string currentOrderStatusId = drv["Id"].ToString();
                        ddlOrderStatus.SelectedValue = currentOrderStatusId;
                    }
                }
            }
        }

        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlOrderStatus = sender as DropDownList;
            RepeaterItem item = ddlOrderStatus.NamingContainer as RepeaterItem;
            if (item != null)
            {
                int orderId = Convert.ToInt32(DataBinder.Eval(item.DataItem, "Id"));
                int newOrderStatusId = Convert.ToInt32(ddlOrderStatus.SelectedValue);

                // Update order status in database
                UpdateOrderStatus(orderId, newOrderStatusId);
            }
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


        private List<OrderStatus> GetOrderStatusList()
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();
            string query = "SELECT Id, Name FROM OrderStatus";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int orderStatusId = Convert.ToInt32(reader["Id"]);
                        string name = reader["Name"].ToString();
                        OrderStatus orderStatus = new OrderStatus(orderStatusId, name);
                        orderStatuses.Add(orderStatus);
                    }
                    con.Close();
                }
            }

            return orderStatuses;
        }

    }
}