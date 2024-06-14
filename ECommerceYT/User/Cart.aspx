<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ECommerceYT.User.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <!-- Cart Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8 table-responsive mb-5">
                <asp:Repeater ID="rptCart" runat="server" OnItemDataBound="rptCart_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-bordered text-center mb-0">
                            <thead class="bg-secondary text-dark">
                                <tr>
                                    <th>Products</th>
                                    <th>Price</th>
                                    <th>Quantity</th>
                                    <th>Size</th>
                                    <th>Color</th>
                                    <th>Total</th>
                                    <th>Remove</th>
                                </tr>
                            </thead>
                            <tbody class="align-middle">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="align-middle">
                                <img src='<%# Eval("ImageUrl") %>' alt="" style="width: 50px;" />
                                <%# Eval("ProductName") %>
                                <asp:HiddenField ID="hfProductId" runat="server" Value='<%# Eval("ProductId") %>' />
                            </td>
                            <td class="align-middle">$<%# Eval("Price") %></td>
                            <td class="align-middle">
                                <div class="input-group quantity mr-3" style="width: 130px;">
                                    <div class="input-group-btn">
                                        <asp:Button ID="btnMinus" runat="server" CssClass="btn btn-primary btn-minus" Text="-" OnClick="btnMinus_Click" CommandArgument='<%# Container.ItemIndex %>' />
                                    </div>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control form-control-sm bg-secondary text-center"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btnPlus" runat="server" CssClass="btn btn-primary btn-plus" Text="+" OnClick="btnPlus_Click" CommandArgument='<%# Container.ItemIndex %>' />
                                    </div>
                                </div>
                            </td>
                            <td class="align-middle"><%# Eval("Size") %></td>
                            <td class="align-middle"><%# Eval("Color") %></td>
                            <td class="align-middle">$<asp:Label ID="lblTotal" runat="server"></asp:Label></td>
                            <td class="align-middle">
                                <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-sm btn-primary" Text="Remove" OnClick="btnRemove_Click" CommandArgument='<%# Eval("ProductID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

            </div>

            <div class="col-lg-4">
                <div class="input-group">
                    <input type="text" class="form-control p-4" placeholder="Coupon Code">
                    <div class="input-group-append">
                        <button class="btn btn-primary">Apply Coupon</button>
                    </div>
                </div>

                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Cart Summary</h4>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-between mb-3 pt-1">
                            <h6 class="font-weight-medium">Subtotal</h6>
                            <asp:Label runat="server"  CssClass="font-weight-medium" id="lblSubtotal">$0.00</asp:Label>
                        </div>
                        <div class="d-flex justify-content-between">
                            <h6 class="font-weight-medium">Shipping</h6>
                            <h6 class="font-weight-medium">$10.00</h6>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <asp:Label runat="server" CssClass="font-weight-bold" id="lblTotal">$10.00</asp:Label>
                        </div>
                        <asp:Button ID="btnCheckout" runat="server" CssClass="btn btn-block btn-primary my-3 py-3" Text="Proceed To Checkout" PostBackUrl="~/User/Checkout.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../UserTemplate/js/jquery-3.6.0.min.js"></script>
    
</asp:Content>
