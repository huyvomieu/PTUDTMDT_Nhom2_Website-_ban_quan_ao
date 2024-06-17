using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using ECommerceYT.Models;

namespace ECommerceYT.User
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.CheckLoginStatus();
            if (!IsPostBack)
            {
                BindCart();
                LoadPaymentMethods();
                LoadShipmentMethods();
                UserInforBind();
            }
        }

        private void UserInforBind()
        {
            var userId = GetUserIdFromSession();
            var userInfo = Utils.GetUserInformation(userId);

            if (userInfo != null)
            {
                txtName.Text = userInfo.Name;
                txtEmail.Text = userInfo.Email;
                txtMobileNo.Text = userInfo.Mobile;
                txtZIPCode.Text = userInfo.PostCode;
                txtCity.Text = userInfo.Address;
            }
        }

        private void BindCart()
        {
            var userId = GetUserIdFromSession();

            string query = @"
                            SELECT 
                                c.ProductId, 
                                c.Quantity, 
                                p.ProductName, 
                                p.Price, 
                                (c.Quantity * p.Price) AS TotalPrice
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
                        rptProducts.DataSource = dt;
                        rptProducts.DataBind();

                        // Tính toán Subtotal
                        decimal subtotal = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            subtotal += Convert.ToDecimal(row["TotalPrice"]);
                        }

                        // Cập nhật các thẻ Subtotal và Total
                        lblSubtotal.Text = "$" + subtotal.ToString("0.00");
                        lblTotal.Text = "$" + (subtotal + 10).ToString("0.00"); // Shipping fixed at $10
                    }
                }
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

        private void LoadPaymentMethods()
        {
            string query = "SELECT PaymentId, PaymentMethod FROM Payment";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ddlPaymentMethod.DataSource = reader;
                        ddlPaymentMethod.DataTextField = "PaymentMethod";
                        ddlPaymentMethod.DataValueField = "PaymentId";
                        ddlPaymentMethod.DataBind();
                    }
                    reader.Close();
                }
            }

            ddlPaymentMethod.Items.Insert(0, new ListItem("Select Payment Method", "0"));
        }

        private void LoadShipmentMethods()
        {
            string query = "SELECT ShipmentId, ShipmentMethod FROM Shipment";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ddlShipment.DataSource = reader;
                        ddlShipment.DataTextField = "ShipmentMethod";
                        ddlShipment.DataValueField = "ShipmentId";
                        ddlShipment.DataBind();
                    }
                    reader.Close();
                }
            }

            ddlShipment.Items.Insert(0, new ListItem("Select Shipment Method", "0"));
        }


        protected void btnOrder_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdFromSession(); // Lấy UserId từ session (hoặc từ đâu đó)

            // Lấy thông tin từ bảng Cart
            DataTable cartItems = GetCartItems(userId);

            if (cartItems != null && cartItems.Rows.Count > 0)
            {
                int orderId = InsertOrder(userId);
                // Lặp qua từng sản phẩm trong giỏ hàng để tạo OrderDetail
                foreach (DataRow row in cartItems.Rows)
                {
                    int productId = Convert.ToInt32(row["ProductId"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);

                    // Lấy giá của sản phẩm từ bảng Product
                    decimal price = GetProductPrice(productId);
                    // Tính tổng tiền cho OrderDetail
                    decimal totalPrice = quantity * price;

                    // Thêm vào bảng OrderDetail
                    InsertOrderDetail(orderId, userId, productId, quantity, totalPrice, totalPrice);
                    UpdateProductQuantityAndSold(productId, quantity);
                }

                // Sau khi thêm tất cả các OrderDetail, có thể xóa các sản phẩm đã đặt hàng khỏi giỏ hàng (Cart)
                ClearCart(userId);
            }

            // Sau khi hoàn thành, có thể chuyển hướng đến trang xác nhận đơn hàng hoặc thông báo thành công
            Response.Redirect("OrderHistoty.aspx");
        }

        private DataTable GetCartItems(int userId)
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT c.ProductId, c.Quantity
            FROM Cart c
            WHERE c.UserId = @UserId";

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

            return dt;
        }

        private decimal GetProductPrice(int productId)
        {
            decimal price = 0;
            string query = @"
            SELECT Price
            FROM Product
            WHERE ProductId = @ProductId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        price = Convert.ToDecimal(result);
                    }
                }
            }

            return price;
        }

        private void InsertOrderDetail(int orderId, int userId, int productId, int quantity, decimal price, decimal totalPrice)
        {
            // Lấy OrderId từ bảng Order sau khi thực hiện INSERT vào bảng Order
            

            string query = @"
                            INSERT INTO OrderDetail (Quantity, Price, ProductId, OrderId, Active)
                            VALUES (@Quantity, @Price, @ProductId, @OrderId, @Active);";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@Active", true);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ClearCart(int userId)
        {
            string query = @"
                            DELETE FROM Cart
                            WHERE UserId = @UserId";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int InsertOrder(int userId)
        {
            string totalText = lblTotal.Text.Replace("$", ""); // Loại bỏ ký tự '$'
            decimal totalPrice = decimal.Parse(totalText); // Chuyển đổi sang decimal
            
            int paymentId = Convert.ToInt32(ddlPaymentMethod.SelectedValue);
            int orderId = 0;
            string query = @"
                            INSERT INTO [Order] (TotalPrice, UserId, PaymentId, Active, OrderStatusId, CreatedTime)
                            VALUES (@TotalPrice, @UserId, @PaymentId, @Active, @OrderStatusId, GETDATE());
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId); // Thay thế 1 bằng PaymentId thực tế
                    cmd.Parameters.AddWithValue("@Active", true);
                    cmd.Parameters.AddWithValue("@OrderStatusId", 1); // Thay thế 1 bằng OrderStatusId thực tế

                    con.Open();
                    // Thực hiện câu lệnh INSERT và lấy ra OrderId của bản ghi vừa chèn
                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return orderId;
        }

        protected void SaveAddress()
        {
            // Lấy UserId từ Session
            int userId = GetUserIdFromSession();

            // Tạo đối tượng Address từ dữ liệu nhập vào form
            Address address = new Address
            {
                UserId = userId,
                AddressLine1 = txtAddressLine1.Text,
                AddressLine2 = txtAddressLine2.Text,
                City = txtCity.Text,
                State = txtState.Text,
                PostalCode = txtZIPCode.Text,
                Country = ddlCountry.SelectedItem.Text // Lấy đất nước từ dropdownlist
            };

            // Gọi hàm để lưu vào CSDL
            SaveAddressToDatabase(address);
            
        }

        private bool SaveAddressToDatabase(Address address)
        {
            // Viết code để lưu address vào database
            try
            {
                // Sử dụng ADO.NET hoặc Entity Framework để thực hiện lưu dữ liệu vào database
                using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                {
                    string query = @"
                        INSERT INTO Address (UserId, AddressLine1, AddressLine2, City, State, PostalCode, Country)
                        VALUES (@UserId, @AddressLine1, @AddressLine2, @City, @State, @PostalCode, @Country);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", address.UserId);
                        cmd.Parameters.AddWithValue("@AddressLine1", address.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", address.AddressLine2);
                        cmd.Parameters.AddWithValue("@City", address.City);
                        cmd.Parameters.AddWithValue("@State", address.State);
                        cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                        cmd.Parameters.AddWithValue("@Country", address.Country);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu có dòng dữ liệu bị ảnh hưởng
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu có
                // Có thể ghi log, thông báo lỗi, hoặc xử lý theo nhu cầu cụ thể
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        private void UpdateProductQuantityAndSold(int productId, int quantityOrdered)
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateProductQuantityAndSold", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@QuantityOrdered", quantityOrdered);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}