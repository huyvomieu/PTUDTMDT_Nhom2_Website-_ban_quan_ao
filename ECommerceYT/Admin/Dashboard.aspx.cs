using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public DataTable dtTopProducts;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load data for top selling products
                dtTopProducts = GetTopSellingProducts();

                // Process data for chart
                StringBuilder sbLabels = new StringBuilder();
                StringBuilder sbData = new StringBuilder();
                StringBuilder sbColors = new StringBuilder();

                int colorIndex = 0;
                string[] chartColors = { "primary", "danger", "cyan", "success", "info" };

                foreach (DataRow row in dtTopProducts.Rows)
                {
                    string productName = row["ProductName"].ToString();
                    decimal totalSales = Convert.ToDecimal(row["TotalSales"]);

                    sbLabels.AppendFormat("'{0}', ", productName);
                    sbData.AppendFormat("{0}, ", totalSales);
                    sbColors.AppendFormat("'{0}', ", chartColors[colorIndex]);

                    colorIndex++;
                    if (colorIndex >= chartColors.Length)
                        colorIndex = 0;
                }

                // Remove trailing commas
                sbLabels.Remove(sbLabels.Length - 2, 2);
                sbData.Remove(sbData.Length - 2, 2);
                sbColors.Remove(sbColors.Length - 2, 2);

                // Inject data into JavaScript function to initialize chart
                string script = string.Format(@"
            <script>
                $(function () {{
                    var options = {{
                        series: [{{
                            name: 'Sales',
                            data: [{0}]
                        }}],
                        chart: {{
                            height: 283,
                            type: 'bar',
                            toolbar: {{
                                show: false
                            }}
                        }},
                        plotOptions: {{
                            bar: {{
                                horizontal: false,
                                columnWidth: '55%',
                                endingShape: 'rounded'
                            }},
                        }},
                        dataLabels: {{
                            enabled: false
                        }},
                        stroke: {{
                            show: true,
                            width: 2,
                            colors: ['transparent']
                        }},
                        xaxis: {{
                            categories: [{1}],
                        }},
                        yaxis: {{
                            title: {{
                                text: 'Sales (USD)'
                            }}
                        }},
                        fill: {{
                            opacity: 1
                        }},
                        colors: [{2}],
                        tooltip: {{
                            y: {{
                                formatter: function (val) {{
                                    return '$' + val
                                }}
                            }}
                        }}
                    }};

                    var chart = new ApexCharts(document.querySelector('#campaign-v2'), options);
                    chart.render();
                }});
            </script>",
                    sbData.ToString(), sbLabels.ToString(), sbColors.ToString());

                // Register script to render the chart
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SalesChart", script);
            }
        }

        public DataTable GetTopSellingProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 4 p.ProductName, SUM(od.Price) AS TotalSales
            FROM OrderDetail od
            INNER JOIN Product p ON od.ProductId = p.ProductId
            GROUP BY p.ProductName
            ORDER BY TotalSales DESC
        ", con))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            return dt;
        }

        public int TotalProduct()
        {
            return Utils.GetAllProducts().Count;
        }

        public int TotalUser()
        {
            return Utils.GetAllCategory().Count;
        }

        public int TotalCategory()
        {
            return Utils.GetAllCategory().Count;
        }

        public decimal GetTotalOrderAmount()
        {
            decimal totalAmount = 0;

            string query = "SELECT SUM(TotalPrice) AS TotalAmount FROM [Order]"; // Thay [Order] bằng tên bảng đơn hàng của bạn

            using (SqlConnection connection = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            totalAmount = Convert.ToDecimal(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý exception theo nhu cầu
                        Console.WriteLine("Error retrieving total order amount: " + ex.Message);
                    }
                }
            }

            return totalAmount;
        }
    }
}