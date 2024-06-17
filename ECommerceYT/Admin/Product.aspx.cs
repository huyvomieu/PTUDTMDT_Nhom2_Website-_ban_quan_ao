using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;

namespace ECommerceYT.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                getProduct();
            }
        }

        private void BindCategories()
        {
            ddlCategory.DataSource = Utils.GetAllCategory();
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataTextField = "CategoryName";

            ddlCategory.DataBind();
        }

        


        private void getProduct()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExcute = false;
            int productId = Convert.ToInt32(hfProductId.Value);
            

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
            cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));
            /*cmd.Parameters.AddWithValue("@Size", ddlSize.SelectedValue);*/
            cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
            /*cmd.Parameters.AddWithValue("@Color", txtColor.Text);*/

            // Retrieve selected sizes
            List<string> sizes = new List<string>();
            foreach (ListItem item in cblSize.Items)
            {
                if (item.Selected)
                {
                    sizes.Add(item.Value);
                }
            }
            cmd.Parameters.AddWithValue("@Size", string.Join(",", sizes));

            // Retrieve selected colors
            List<string> colors = new List<string>();
            foreach (ListItem item in cblColor.Items)
            {
                if (item.Selected)
                {
                    colors.Add(item.Value);
                }
            }
            cmd.Parameters.AddWithValue("@Color", string.Join(",", colors));

            cmd.Parameters.AddWithValue("@IsActive", cbbIsActive.Checked);
            if (fuProductImage.HasFile)
            {
                if (Utils.isValidExtension(fuProductImage.FileName))
                {
                    //save image
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/Product/" + newImageName.ToString() + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + newImageName.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "plese select .jpg, .jpge, .png file";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExcute = false;
                }
            }
            else
            {
                isValidToExcute = true;
            }

            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = productId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product" + actionName + "succesfully";
                    lblMsg.CssClass = "alert alert-success";
                    Clear();
                    getProduct();
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
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
            cbbIsActive.Checked = false;
            hfProductId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;

            // Clear checkboxes
            foreach (ListItem item in cblSize.Items)
            {
                item.Selected = false;
            }
            foreach (ListItem item in cblColor.Items)
            {
                item.Selected = false;
            }

        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                rProduct.DataSource = dt;
                rProduct.DataBind();
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                /*ddlSize.SelectedValue = dt.Rows[0]["Size"].ToString();
                txtColor.Text = dt.Rows[0]["Color"].ToString();*/

                // Select the sizes in the checkbox list
                string[] sizes = dt.Rows[0]["Size"].ToString().Split(',');
                foreach (string size in sizes)
                {
                    ListItem item = cblSize.Items.FindByValue(size);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                // Select the colors in the checkbox list
                string[] colors = dt.Rows[0]["Color"].ToString().Split(',');
                foreach (string color in colors)
                {
                    ListItem item = cblColor.Items.FindByValue(color);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }



                ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();

                cbbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Images/No_Images.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfProductId.Value = dt.Rows[0]["ProductId"].ToString();
                btnAddOrUpdate.Text = "Updated";
                getProduct(); // Refresh the category list
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product deleted successfully";
                    lblMsg.CssClass = "alert alert-success";
                    getProduct(); // Refresh the category list
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
    }
}