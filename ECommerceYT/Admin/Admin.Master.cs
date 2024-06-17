using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECommerceYT.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckLogin();
        }

        public string GetImageUrlFromSession()
        {
            object imageUrl = Session["ImageUrlAdmin"];
            return Utils.GetImageUrl(imageUrl);
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        public void Logout()
        {
            // Xóa tất cả thông tin trong Session
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("~/Admin/Login.aspx");
        }

        private void CheckLogin()
        {
            if (Session["UserNameAdmin"] == null)
            {
                Response.Redirect("~/Admin/Login.aspx");
            }
        }
    }
}