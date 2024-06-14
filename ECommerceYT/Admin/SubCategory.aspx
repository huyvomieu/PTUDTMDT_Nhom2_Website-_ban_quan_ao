<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SubCategory.aspx.cs" Inherits="ECommerceYT.Admin.SubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%= lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Sub Category</h4>
                    <hr />
                    <div class="form-body">
                        <label>Sub Category Name</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtSubCategoryName" runat="server" CssClass="form-control" placeholder="Enter Sub Category Name"></asp:TextBox>
                                    <asp:HiddenField ID="hfSubCategoryId" Value="0" runat="server" />
                                    <asp:RequiredFieldValidator ID="rvfSubCategoryName" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtSubCategoryName"
                                        ErrorMessage="Sub Category Name is required">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-body">
                        <label>Category</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCategory" runat="server" size="1"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:CheckBox ID="cbbIsActive" runat="server" Text="&nbsp; IsActive" />
                            </div>
                        </div>
                    </div>


                    <div class="form-actiona pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" OnClick="btnClear_Click" Text="Reset" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-8">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Sub Category List</h4>
                <hr />
                <div class="table-responsive">
                    <asp:Repeater ID="rSubCategory" runat="server" OnItemCommand="rCategory_ItemCommand">
                        <HeaderTemplate>
                            <table class="table data-table-export table-hover nowrap">
                                <thead>
                                    <tr>
                                        <th class="table-plus">SubCategory Name</th>
                                        <th>Category</th>
                                        <th>Active</th>
                                        <th>Created Date</th>
                                        <th class="datatabe-nosort">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="table-plus"><%# Eval("SubCategoryName") %> </td>
                                <td class="table-plus"><%# Eval("CategoryId") %> </td>
                                <td>
                                    <asp:Label ID="lblIsActive" runat="server"
                                        Text='<%# ECommerceYT.Utils.GetIsActiveText(Eval("IsActive")) %>'
                                        CssClass='<%# ECommerceYT.Utils.GetIsActiveCssClass(Eval("IsActive")) %>'>
                                    </asp:Label>
                                </td>
                                <td><%# Eval("CreatedDate") %></td>
                                <td>
                                    <asp:LinkButton CommandName="edit" runat="server" ID="lbEdit" Text="Edit" CssClass="badge badge-primary" CommandArgument='<%#Eval("SubCategoryId") %>' CausesValidation="false">
                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton CommandName="delete" runat="server" ID="lbDelete" Text="Delete" CssClass="badge badge-danger" CommandArgument='<%#Eval("SubCategoryId") %>' CausesValidation="false">
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


</asp:Content>
