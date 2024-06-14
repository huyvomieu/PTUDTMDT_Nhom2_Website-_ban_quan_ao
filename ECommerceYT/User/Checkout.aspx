<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="ECommerceYT.User.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../UserTemplate/js/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= ddlPaymentMethod.ClientID %>').change(function () {
                var selectedValue = $(this).val();
                if (selectedValue == "1") { // BankTransfer has ID = 1
                    $('#paymentImage').attr('src', '../Images/Payment.jpg').show();
                } else {
                    $('#paymentImage').hide();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Checkout</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="">Home</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Checkout</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->


    <!-- Checkout Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8">
                <div class="mb-4">
                    <h4 class="font-weight-semi-bold mb-4">Billing Address</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="HuyKhoi"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>E-mail</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="example@email.com"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Mobile No</label>
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="+123 456 789"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Address Line 1</label>
                            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" placeholder="123 Street"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Address Line 2</label>
                            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control" placeholder="123 Street"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Country</label>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="custom-select">
                                <asp:ListItem Text="United States"></asp:ListItem>
                                <asp:ListItem Text="Afghanistan"></asp:ListItem>
                                <asp:ListItem Text="Albania"></asp:ListItem>
                                <asp:ListItem Text="Algeria"></asp:ListItem>
                                <asp:ListItem Text="VietNam" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="New York"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>State</label>
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="New York"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>ZIP Code</label>
                            <asp:TextBox ID="txtZIPCode" runat="server" CssClass="form-control" placeholder="123"></asp:TextBox>
                        </div>
                        
                    </div>
                </div>

                <div class="collapse mb-4" id="shipping-address">
                    <h4 class="font-weight-semi-bold mb-4">Shipping Address</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>First Name</label>
                            <input class="form-control" type="text" placeholder="John">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Last Name</label>
                            <input class="form-control" type="text" placeholder="Doe">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>E-mail</label>
                            <input class="form-control" type="text" placeholder="example@email.com">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Mobile No</label>
                            <input class="form-control" type="text" placeholder="+123 456 789">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Address Line 1</label>
                            <input class="form-control" type="text" placeholder="123 Street">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Address Line 2</label>
                            <input class="form-control" type="text" placeholder="123 Street">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Country</label>
                            <select class="custom-select">
                                <option selected>United States</option>
                                <option>Afghanistan</option>
                                <option>Albania</option>
                                <option>Algeria</option>
                            </select>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>City</label>
                            <input class="form-control" type="text" placeholder="New York">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>State</label>
                            <input class="form-control" type="text" placeholder="New York">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>ZIP Code</label>
                            <input class="form-control" type="text" placeholder="123">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Order Total</h4>
                    </div>
                    <div class="card-body">
                        <h5 class="font-weight-medium mb-3">Products</h5>
                        <asp:Repeater ID="rptProducts" runat="server">
                            <ItemTemplate>
                                <div class="d-flex justify-content-between">
                                    <p><%# Eval("ProductName") %></p>
                                    <p>$<%# Eval("TotalPrice", "{0:N2}") %></p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <hr class="mt-0">
                        <div class="d-flex justify-content-between mb-3 pt-1">
                            <h6 class="font-weight-medium">Subtotal</h6>
                            <h6 class="font-weight-medium">
                                <asp:Label ID="lblSubtotal" runat="server" Text="$0.00"></asp:Label>
                            </h6>
                        </div>
                        <div class="d-flex justify-content-between">
                            <h6 class="font-weight-medium">Shipping</h6>
                            <h6 class="font-weight-medium">$10.00</h6>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <h5 class="font-weight-bold">
                                <asp:Label ID="lblTotal" runat="server" Text="$10.00"></asp:Label>
                            </h5>
                        </div>
                    </div>
                </div>

                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Shipment</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="ddlShipment">Select Shipment Method</label>
                            <asp:DropDownList ID="ddlShipment" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Payment</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="ddlPaymentMethod">Select Payment Method</label>
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <img id="paymentImage" src="" alt="Payment Method" style="display: none;" class="img-fluid" />
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <asp:Button runat="server" ID="btnOrder" CssClass="btn btn-lg btn-block btn-primary font-weight-bold my-3 py-3" Text="Place Order" OnClick="btnOrder_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Checkout End -->
</asp:Content>
