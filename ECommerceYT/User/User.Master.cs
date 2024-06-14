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
                loginBtnMaster.Visible = false;
                registerBtnMaster.Visible = false;
                userInfoPanel.Visible = true;
                litUserInfo.Text = $"Welcome, {username}!";
            }
            else
            {
                loginBtnMaster.Visible = true;
                registerBtnMaster.Visible = true;
                userInfoPanel.Visible = false;
                litUserInfo.Text = string.Empty;
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
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

    }
}