using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECommerceYT.Admin;
using ECommerceYT.Models;

namespace ECommerceYT.User
{
    public partial class Shop : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getProduct(1);
            }
        }

        protected void rptPager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int pageIndex = Convert.ToInt32(e.CommandArgument);
                getProduct(pageIndex);
                CurrentPage = pageIndex;
            }
        }


        protected string ResolveImageUrl(object imageUrl)
        {
            return ECommerceYT.Utils.GetImageUrl(imageUrl);
        }

        private void getProduct(int pageNumber)
        {
            int pageSize = 9; // Số sản phẩm mỗi trang

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Product_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GETBYPAGE");
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptProducts.DataSource = dt;
                        rptProducts.DataBind();
                    }
                }
            }

            // Hiển thị liên kết phân trang
            PopulatePager(pageNumber);
        }


        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                    return (int)ViewState["CurrentPage"];
                else
                    return 1;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected string GetPagerCssClass(int pageIndex)
        {
            return pageIndex == CurrentPage ? "page-item active" : "page-item";
        }

        private void PopulatePager(int pageNumber)
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Product", con))
                {
                    con.Open();
                    int totalRecords = (int)cmd.ExecuteScalar();
                    con.Close();

                    int pageSize = 9;
                    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                    DataTable dtPager = new DataTable();
                    dtPager.Columns.Add("PageIndex");
                    dtPager.Columns.Add("PageText");

                    for (int i = 1; i <= totalPages; i++)
                    {
                        DataRow dr = dtPager.NewRow();
                        dr[0] = i;
                        dr[1] = i;
                        dtPager.Rows.Add(dr);
                    }

                    rptPager.DataSource = dtPager;
                    rptPager.DataBind();
                }
            }
        }


        public string GetTimeAgo(object createdDate)
        {
            DateTime dt = (DateTime)createdDate;
            TimeSpan span = DateTime.Now.Subtract(dt);

            if (span.Days > 365)
            {
                int years = span.Days / 365;
                return $"{years} year{(years > 1 ? "s" : "")} ago";
            }
            if (span.Days > 30)
            {
                int months = span.Days / 30;
                return $"{months} month{(months > 1 ? "s" : "")} ago";
            }
            if (span.Days > 0)
            {
                return $"{span.Days} day{(span.Days > 1 ? "s" : "")} ago";
            }
            if (span.Hours > 0)
            {
                return $"{span.Hours} hour{(span.Hours > 1 ? "s" : "")} ago";
            }
            if (span.Minutes > 0)
            {
                return $"{span.Minutes} minute{(span.Minutes > 1 ? "s" : "")} ago";
            }
            return "Just now";
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sortBy = ddlSortBy.SelectedValue;
            List<ECommerceYT.Models.Product> products = new List<ECommerceYT.Models.Product>();

            if (sortBy == "Latest")
            {
                // Lấy danh sách sản phẩm sắp xếp theo thời gian tạo mới nhất
                products = Utils.GetProductsSortedByCreatedDate();
            }
            else if (sortBy == "Popularity")
            {
                // Lấy danh sách sản phẩm sắp xếp theo số lượng đã bán
                products = Utils.GetProductsSortedBySold();
            }

            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                DataTable dt = SearchByName(searchKeyword); // Gọi hàm tìm kiếm theo tên

                // Bind kết quả tìm kiếm vào Repeater hoặc GridView
                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
        }

        private DataTable SearchByName(string searchKeyword)
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Product_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "SEARCHBYNAME");
                    cmd.Parameters.AddWithValue("@ProductName", searchKeyword);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMinPrice.Text) && !string.IsNullOrEmpty(txtMaxPrice.Text))
            {
                try
                {
                    var minPrice = decimal.Parse(txtMinPrice.Text);
                    var maxPrice = decimal.Parse(txtMaxPrice.Text);

                    // Logging giá trị đầu vào
                    System.Diagnostics.Debug.WriteLine($"minPrice: {minPrice}, maxPrice: {maxPrice}");

                    DataTable dt = SearchByPriceRange(minPrice, maxPrice); // Gọi hàm tìm kiếm 


                    // Bind kết quả tìm kiếm vào Repeater hoặc GridView
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
                catch (FormatException ex)
                {
                    // Logging lỗi định dạng
                    System.Diagnostics.Debug.WriteLine($"Error parsing prices: {ex.Message}");
                }
            }
        }


        private DataTable SearchByPriceRange(decimal minPrice, decimal maxPrice)
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                string query = "SELECT * FROM Product WHERE Price > @MinPrice AND Price < @MaxPrice";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                    cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    }
}