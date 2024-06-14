<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="ECommerceYT.Admin.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%= lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imagePreview.ClientID %>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">User</h4>
                    <hr />

                    <div class="form-body">
                        <label>Name</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfName" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtName"
                                        ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>User Name</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter User Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfUserName" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtUserName"
                                        ErrorMessage="User Name is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Mobile</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfMobile" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtMobile"
                                        ErrorMessage="Mobile is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-body">
                        <label>Email</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfEmail" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtEmail"
                                        ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-body">
                        <label>PostCode</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" placeholder="PostCode Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfPostCode" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPostCode"
                                        ErrorMessage="PostCode is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-body">
                        <label>Address</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfAddress" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtAddress"
                                        ErrorMessage="Address is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-body">
                        <label>Password</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfPassword" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPassword"
                                        ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Role</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlRoles" runat="server" size="1"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Category Image</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                    <asp:HiddenField ID="hfUserId" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-actiona pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" OnClick="btnClear_Click" Text="Reset" />
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" AlternateText="" />
                    </div>

                </div>
            </div>
        </div>


    </div>

    <div class="row mt-4">
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">User List</h4>
                    <hr />
                    <div class="tab-content table-responsive">
                        <asp:Repeater ID="rUser" runat="server" OnItemCommand="rCategory_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Name</th>
                                            <th class="table-plus">UserName</th>
                                            <th class="table-plus">Mobile</th>
                                            <th class="table-plus">Email</th>
                                            <th class="table-plus">Address</th>
                                            <th class="table-plus">PostCode</th>
                                            <th class="table-plus">Password</th>
                                            <th>Image</th>
                                            <th>Role</th>
                                            <th>Created Date</th>
                                            <th class="datatabe-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("Name") %> </td>
                                    <td class="table-plus"><%# Eval("Username") %> </td>
                                    <td class="table-plus"><%# Eval("Mobile") %> </td>
                                    <td class="table-plus"><%# Eval("Email") %> </td>
                                    <td class="table-plus"><%# Eval("Address") %> </td>
                                    <td class="table-plus"><%# Eval("PostCode") %> </td>
                                    <td class="table-plus"><%# Eval("Password") %> </td>
                                    <td>
                                        <img width="40" src='<%# ECommerceYT.Utils.GetImageUrl(Eval("ImageUrl")) %>' />
                                    </td>
                                    <td class="table-plus"><%# Eval("RoleId") %> </td>
                                    <td><%# Eval("CreatedDate") %></td>
                                    <td>
                                        <asp:LinkButton CommandName="edit" runat="server" ID="lbEdit" Text="Edit" CssClass="badge badge-primary" CommandArgument='<%#Eval("UserId") %>' CausesValidation="false">
                                 <i class="fas fa-edit"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton CommandName="delete" runat="server" ID="lbDelete" Text="Delete" CssClass="badge badge-danger" CommandArgument='<%#Eval("UserId") %>' CausesValidation="false">
                                 <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                 </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
