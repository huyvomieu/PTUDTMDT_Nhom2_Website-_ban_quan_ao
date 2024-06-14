using ECommerceYT.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ECommerceYT.Admin
{
    public partial class HomeAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTopSellingProducts();
                BindBadSellingProducts();
                BindDailyStatistics();
                BindMonthlyStatistics();
                BindYearlyStatistics();
            }
        }

        private void BindTopSellingProducts()
        {

            List<TopSellingProduct> topProducts = new List<TopSellingProduct>();

            // Connect to database and execute query
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = @"
                SELECT TOP 5
                    p.ProductId,
                    p.Sold,
                    SUM(od.Price) AS TotalMoneyEarned,
                    SUM(CASE WHEN o.CreatedTime >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END) AS SoldThisWeek,
                    SUM(CASE WHEN o.CreatedTime >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END) AS SoldThisMonth,
                    SUM(CASE WHEN o.CreatedTime >= DATEADD(YEAR, DATEDIFF(YEAR, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END) AS SoldThisYear
                FROM Product p
                INNER JOIN OrderDetail od ON p.ProductId = od.ProductId
                INNER JOIN [Order] o ON od.OrderId = o.Id
                GROUP BY p.ProductId, p.Sold
                ORDER BY SUM(od.Quantity) DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TopSellingProduct product = new TopSellingProduct();
                        product.ProductId = Convert.ToInt32(reader["ProductId"]);
                        product.Sold = Convert.ToInt32(reader["Sold"]);
                        product.TotalMoneyEarned = Convert.ToDecimal(reader["TotalMoneyEarned"]);
                        product.SoldThisWeek = Convert.ToInt32(reader["SoldThisWeek"]);
                        product.SoldThisMonth = Convert.ToInt32(reader["SoldThisMonth"]);
                        product.SoldThisYear = Convert.ToInt32(reader["SoldThisYear"]);

                        topProducts.Add(product);
                    }
                    con.Close();
                }
            }

            rptTopSellingProducts.DataSource = topProducts;
            rptTopSellingProducts.DataBind();
        }

        private void BindBadSellingProducts()
        {

            List<TopSellingProduct> topProducts = new List<TopSellingProduct>();

            // Connect to database and execute query
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = @"
                    SELECT TOP 5
                        p.ProductId,
                        ISNULL(SUM(od.Quantity), 0) AS TotalSold,
                        SUM(od.Price) AS TotalMoneyEarned,
                        ISNULL(SUM(CASE WHEN o.CreatedTime >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END), 0) AS SoldThisWeek,
                        ISNULL(SUM(CASE WHEN o.CreatedTime >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END), 0) AS SoldThisMonth,
                        ISNULL(SUM(CASE WHEN o.CreatedTime >= DATEADD(YEAR, DATEDIFF(YEAR, 0, GETDATE()), 0) THEN od.Quantity ELSE 0 END), 0) AS SoldThisYear
                    FROM Product p
                    LEFT JOIN OrderDetail od ON p.ProductId = od.ProductId
                    LEFT JOIN [Order] o ON od.OrderId = o.Id
                    GROUP BY p.ProductId
                    ORDER BY TotalSold ASC";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TopSellingProduct product = new TopSellingProduct();
                        product.ProductId = Convert.ToInt32(reader["ProductId"]);
                        product.Sold = Convert.ToInt32(reader["TotalSold"]);
                        decimal totalMoneyEarned = 0;

                        if (!Convert.IsDBNull(reader["TotalMoneyEarned"]))
                        {
                            totalMoneyEarned = Convert.ToDecimal(reader["TotalMoneyEarned"]);
                        }
                        product.SoldThisWeek = Convert.ToInt32(reader["SoldThisWeek"]);
                        product.SoldThisMonth = Convert.ToInt32(reader["SoldThisMonth"]);
                        product.SoldThisYear = Convert.ToInt32(reader["SoldThisYear"]);

                        topProducts.Add(product);
                    }
                    con.Close();
                }
            }

            rptBad.DataSource = topProducts;
            rptBad.DataBind();
        }

        private void BindDailyStatistics()
        {
            string query = @"
        SELECT 
            CONVERT(date, CreatedTime) AS Date,
            COUNT(*) AS TotalOrders,
            SUM(TotalPrice) AS TotalRevenue
        FROM [Order]
        WHERE YEAR(CreatedTime) = YEAR(GETDATE()) AND MONTH(CreatedTime) = MONTH(GETDATE())
        GROUP BY CONVERT(date, CreatedTime)
        ORDER BY Date DESC";

            BindRepeater(query, rptDailyStats);
        }

        private void BindMonthlyStatistics()
        {
            string query = @"
        SELECT 
            DATENAME(month, CreatedTime) AS [Month],
            COUNT(*) AS TotalOrders,
            SUM(TotalPrice) AS TotalRevenue
        FROM [Order]
        WHERE YEAR(CreatedTime) = YEAR(GETDATE())
        GROUP BY DATENAME(month, CreatedTime), MONTH(CreatedTime)
        ORDER BY MONTH(CreatedTime) DESC";

            BindRepeater(query, rptMonthlyStats);
        }

        private void BindYearlyStatistics()
        {
            string query = @"
        SELECT 
            YEAR(CreatedTime) AS [Year],
            COUNT(*) AS TotalOrders,
            SUM(TotalPrice) AS TotalRevenue
        FROM [Order]
        GROUP BY YEAR(CreatedTime)
        ORDER BY [Year] DESC";

            BindRepeater(query, rptYearlyStats);
        }

        private void BindRepeater(string query, Repeater repeater)
        {
            string connectionString = Utils.getConnection();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    repeater.DataSource = dt;
                    repeater.DataBind();
                }
            }
        }

       

    }
}