<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="UserInfor.aspx.cs" Inherits="ECommerceYT.User.UserInfor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row">
        <div class="card-body">
            <div class="col-md-8 m-auto">
                <h2>User Information</h2>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <table>
                    <tr>
                        <td>User ID:</td>
                        <td>
                            <asp:TextBox ID="txtUserId" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Name:</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Username:</td>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Mobile:</td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Address:</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Post Code:</td>
                        <td>
                            <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Image URL:</td>
                        <td>
                            <asp:TextBox ID="txtImageUrl" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Old Password:</td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>New Password:</td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Re-enter New Password:</td>
                        <td>
                            <asp:TextBox ID="txtReEnterNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button CssClass="btn btn-success" ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button CssClass="btn btn-danger" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
