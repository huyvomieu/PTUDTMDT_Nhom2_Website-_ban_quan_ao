using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.Admin
{
    public partial class SubCategory : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            if (!IsPostBack)
            {
                getSubCategory();
                BindCategory();
            }
        }

        private void BindCategory()
        {
            ddlCategory.DataSource = Utils.GetAllCategory();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
        }

        private void getSubCategory()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SubCategory_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rSubCategory.DataSource = dt;
            rSubCategory.DataBind();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            bool isValidToExcute = false;
            int userId = Convert.ToInt32(hfSubCategoryId.Value);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SubCategory_Crud", con);
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@SubCategoryId", userId);
            cmd.Parameters.AddWithValue("@SubCategoryName", txtSubCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
            cmd.Parameters.AddWithValue("@IsActive", cbbIsActive.Checked);
            isValidToExcute = true;

            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sub Category " + actionName + " successfully";
                    lblMsg.CssClass = "alert alert-success";
                    getSubCategory();
                    Clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtSubCategoryName.Text = string.Empty;
            cbbIsActive.Checked = false;
            ddlCategory.SelectedIndex = 0;
            hfSubCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;

            if (e.CommandName == "edit")
            {
                using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("SubCategory_Crud", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "GETBYID");
                        cmd.Parameters.AddWithValue("@SubCategoryId", Convert.ToInt32(e.CommandArgument));

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            txtSubCategoryName.Text = dt.Rows[0]["SubCategoryName"].ToString();
                            cbbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                            ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                            hfSubCategoryId.Value = dt.Rows[0]["SubCategoryId"].ToString();
                            btnAddOrUpdate.Text = "Update";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Sub Category not found.";
                            lblMsg.CssClass = "alert alert-warning";
                        }
                    }
                }
            }
            else if (e.CommandName == "delete")
            {
                using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("SubCategory_Crud", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "DELETE");
                        cmd.Parameters.AddWithValue("@SubCategoryId", Convert.ToInt32(e.CommandArgument));

                        try
                        {
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Sub Category deleted successfully";
                                lblMsg.CssClass = "alert alert-success";
                                getSubCategory();
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Sub Category not found or could not be deleted.";
                                lblMsg.CssClass = "alert alert-warning";
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = ex.Message;
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
            }
        }
    }
}
