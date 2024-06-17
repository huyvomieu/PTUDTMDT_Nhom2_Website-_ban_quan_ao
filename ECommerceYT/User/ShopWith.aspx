<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ShopWith.aspx.cs" Inherits="ECommerceYT.User.ShopWith" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .product-img {
            width: 100%;
            height: 300px; /* Đảm bảo chiều cao cố định */
            overflow: hidden;
        }

            .product-img img {
                width: 100%;
                height: 100%;
                object-fit: cover; /* Đảm bảo ảnh không bị méo và lấp đầy khung */
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="col-lg-9 col-md-12">
        <div class="row pb-3">
            <asp:Repeater ID="rptProducts" runat="server">
                <ItemTemplate>
                    <div class="col-lg-4 col-md-6 col-sm-12 pb-1">
                        <div class="card product-item border-0 mb-4">
                            <div class="card-header product-img position-relative overflow-hidden bg-transparent border p-0">
                                <img class="img-fluid" src='<%# ResolveImageUrl(Eval("ImageUrl")) %>' alt='<%# Eval("ProductName") %>'>
                            </div>
                            <div class="card-body border-left border-right text-center p-0 pt-4 pb-3">
                                <h6 class="text-truncate mb-3"><%# Eval("ProductName") %></h6>
                                <div class="d-flex justify-content-center">
                                    <h6>$<%# Eval("Price", "{0:0.00}") %></h6>
                                </div>
                                <p>Sold: <%# Eval("Sold") %></p>
                                <p>Created: <%# GetTimeAgo(Eval("CreatedDate")) %></p>
                            </div>
                            <div class="card-footer d-flex justify-content-between bg-light border">
                                <a href='<%# "ShopDetail.aspx?ProductId=" + Eval("ProductId") %>' class="btn btn-sm text-dark p-0"><i class="fas fa-eye text-primary mr-1"></i>View Detail</a>
                                <a href="#" class="btn btn-sm text-dark p-0"><i class="fas fa-shopping-cart text-primary mr-1"></i>Add To Cart</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>
