<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="OrderHistoty.aspx.cs" Inherits="ECommerceYT.User.OrderHistoty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container">
        <h1 class="mt-4">Order History</h1>
        <hr />
        <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowCommand="gvOrders_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Order ID" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="OrderStatus" HeaderText="Status" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />

                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="badge badge-primary" ID="btnViewDetail" CommandName="ViewDetails" runat="server" Text="View Detail" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cancel Order">
                    <ItemTemplate>
                        <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel" CssClass="btn btn-danger"
                            CommandName="CancelOrder" CommandArgument='<%# Eval("Id") %>'
                            Visible='<%# Eval("OrderStatus").ToString()  == "Pending" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div>
        <h2 >Order Detail</h2>
        <asp:GridView ID="gvOrderDetails" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Detail ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
