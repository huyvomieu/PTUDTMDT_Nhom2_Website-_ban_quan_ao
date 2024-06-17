using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace ECommerceYT.Admin
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int GetOrderCountForDay(DateTime date)
        {
            int orderCount = 0;
            // Kết nối cơ sở dữ liệu và thực hiện truy vấn để đếm số đơn hàng cho ngày này
            // Đây là ví dụ đơn giản, bạn cần điều chỉnh phù hợp với cấu trúc cơ sở dữ liệu của bạn
            
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT COUNT(*) FROM [Order] WHERE CONVERT(date, CreatedTime) = @Date";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    con.Open();
                    orderCount = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            return orderCount;
        }

    }
}