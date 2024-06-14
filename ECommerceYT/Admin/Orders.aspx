<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ECommerceYT.Admin.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-12 col-md-8">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Order List</h4>
                <hr />
                <div class="table-responsive">
                    <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rCategory_ItemCommand" OnItemDataBound="rProduct_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table data-table-export table-hover nowrap">
                                <thead>
                                    <tr>
                                        <th class="table-plus">Order Code</th>
                                        <th>Total Price</th>
                                        <th>UserId</th>
                                        <th>Payment Method</th>
                                        <th>Order Status</th>
                                        <th>Created Time</th>
                                        <th class="datatabe-nosort">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="table-plus"><%# Eval("Id") %> </td>
                                <td><%# Eval("TotalPrice") %></td>
                                <td><%# Eval("UserId") %></td>
                                <td><%# Eval("PaymentMethod") %></td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td><%# Eval("CreatedTime") %></td>
                                <td>
                                    <asp:LinkButton CommandName="edit" runat="server" ID="lbEdit" Text="Edit" CssClass="badge badge-primary"
                                        CommandArgument='<%#Eval("Id") %>' CausesValidation="false">
                    <i class="fas fa-edit"></i>
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
