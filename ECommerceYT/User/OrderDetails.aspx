<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="ECommerceYT.User.OrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container">
            <h1 class="mt-4">Order Detail</h1>
            <hr />
            <div class="row">
                <div class="col-md-6">
                    <h4>Order Information</h4>
                    <div class="mb-3">
                        <strong>Order ID:</strong> <span id="lblOrderId" runat="server"></span>
                    </div>
                    <div class="mb-3">
                        <strong>Total Price:</strong> <span id="lblTotalPrice" runat="server"></span>
                    </div>
                    <div class="mb-3">
                        <strong>Order Status:</strong> <span id="lblOrderStatus" runat="server"></span>
                    </div>
                </div>
                <div class="col-md-6">
                    <h4>Customer Information</h4>
                    <div class="mb-3">
                        <strong>Name:</strong> <span id="lblCustomerName" runat="server"></span>
                    </div>
                    <div class="mb-3">
                        <strong>Email:</strong> <span id="lblCustomerEmail" runat="server"></span>
                    </div>
                    <div class="mb-3">
                        <strong>Mobile Number:</strong> <span id="lblCustomerMobile" runat="server"></span>
                    </div>
                </div>
            </div>
            <hr />
            <h4>Order Items</h4>
            <asp:GridView ID="gvOrderItems" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ProductId" HeaderText="Product ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
