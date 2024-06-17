using ECommerceYT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.User
{
    public partial class User : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                //load the control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }

            if (!IsPostBack)
            {
                UpdateHeader();
                BindCategories();
            }
        }

        private void UpdateHeader()
        {
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();
                //an btn log in
                loginBtnMaster.Visible = false;
                registerBtnMaster.Visible = false;
                userInfoPanel.Visible = true;

            }
            else
            {
                loginBtnMaster.Visible = true;
                registerBtnMaster.Visible = true;
                userInfoPanel.Visible = false;

            }
        }

        public string GetImageUrlFromSession()
        {
            object imageUrl = Session["ImageUrl"];
            return Utils.GetImageUrl(imageUrl);
        }

        public void Logout()
        {
            // Xóa tất cả thông tin trong Session
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("Default.aspx");
        }

        private List<Category> GetCategoriesFromDatabase()
        {
            // Replace this with your actual database retrieval logic
            List<Category> categories = new List<Category>();
            string query = "SELECT CategoryName FROM Category WHERE IsActive = 1";

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                }
            }

            return categories;
        }

        private void BindCategories()
        {
            // Assume you have a list of Category objects with CategoryName
            List<Category> categories = GetCategoriesFromDatabase(); // Replace with your actual method to fetch categories

            rptCategories.DataSource = categories;
            rptCategories.DataBind();
        }

        protected int GetUserId()
        {
            if (Session["UserName"] != null)
            {
                var username = Session["UserName"].ToString();
                var id = Utils.GetUserIdFromSession(username);
                return id;
            }
            return 1300;
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}