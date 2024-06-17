<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="ECommerceYT.User.Shop" %>

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

    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Our Shop</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="">Home</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Shop</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->


    <!-- Shop Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <!-- Shop Sidebar Start -->
            <div class="col-lg-3 col-md-12">
                <!-- Price Start -->
                <div class="border-bottom mb-4 pb-4">
                    <h5 class="font-weight-semi-bold mb-4">Filter by price</h5>

                    <div class="form-row">
                        <div class="col-md-6 mb-3">
                            <label for="minPrice">Min Price</label>
                            <asp:TextBox ID="txtMinPrice" runat="server" CssClass="form-control" placeholder="$" onkeypress="return validateNumberInput(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="maxPrice">Max Price</label>
                            <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="form-control" placeholder="$" onkeypress="return validateNumberInput(event)"></asp:TextBox>
                        </div>
                    </div>

                    <asp:LinkButton ID="btnApplyFilter" runat="server" OnClick="btnApplyFilter_Click" CssClass="btn btn-primary">Apply Filter</asp:LinkButton>
                </div>


                <!-- Price End -->

                <!-- Color Start -->
                <div class="border-bottom mb-4 pb-4">
                    <h5 class="font-weight-semi-bold mb-4">Filter by color</h5>

                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" checked id="color-all">
                        <label class="custom-control-label" for="price-all">All Color</label>
                        <span class="badge border font-weight-normal">1000</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="color-1">
                        <label class="custom-control-label" for="color-1">Black</label>
                        <span class="badge border font-weight-normal">150</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="color-2">
                        <label class="custom-control-label" for="color-2">White</label>
                        <span class="badge border font-weight-normal">295</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="color-3">
                        <label class="custom-control-label" for="color-3">Red</label>
                        <span class="badge border font-weight-normal">246</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="color-4">
                        <label class="custom-control-label" for="color-4">Blue</label>
                        <span class="badge border font-weight-normal">145</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between">
                        <input type="checkbox" class="custom-control-input" id="color-5">
                        <label class="custom-control-label" for="color-5">Green</label>
                        <span class="badge border font-weight-normal">168</span>
                    </div>

                </div>
                <!-- Color End -->

                <!-- Size Start -->
                <div class="mb-5">
                    <h5 class="font-weight-semi-bold mb-4">Filter by size</h5>

                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" checked id="size-all">
                        <label class="custom-control-label" for="size-all">All Size</label>
                        <span class="badge border font-weight-normal">1000</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="size-1">
                        <label class="custom-control-label" for="size-1">XS</label>
                        <span class="badge border font-weight-normal">150</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="size-2">
                        <label class="custom-control-label" for="size-2">S</label>
                        <span class="badge border font-weight-normal">295</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="size-3">
                        <label class="custom-control-label" for="size-3">M</label>
                        <span class="badge border font-weight-normal">246</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between mb-3">
                        <input type="checkbox" class="custom-control-input" id="size-4">
                        <label class="custom-control-label" for="size-4">L</label>
                        <span class="badge border font-weight-normal">145</span>
                    </div>
                    <div class="custom-control custom-checkbox d-flex align-items-center justify-content-between">
                        <input type="checkbox" class="custom-control-input" id="size-5">
                        <label class="custom-control-label" for="size-5">XL</label>
                        <span class="badge border font-weight-normal">168</span>
                    </div>

                </div>
                <!-- Size End -->
            </div>
            <!-- Shop Sidebar End -->


            <!-- Shop Product Start -->
            <div class="col-lg-9 col-md-12">
                <div class="row pb-3">
                    <div class="col-12 pb-1">
                        <div class="d-flex align-items-center justify-content-between mb-4">

                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Search by name"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="input-group-text bg-transparent text-primary" OnClick="btnSearch_Click">
                                            <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="dropdown ml-4">
                                <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="btn border" AutoPostBack="True" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                                    <asp:ListItem Text="Sort by" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Latest" Value="Latest"></asp:ListItem>
                                    <asp:ListItem Text="Popularity" Value="Popularity"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
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
                                        <div class="d-flex justify-content-center">
                                            <h6>Quantity: <%# Eval("Quantity") %></h6>
                                        </div>
                                        <p>Sold: <%# Eval("Sold") %></p>
                                        <p>Created: <%# GetTimeAgo(Eval("CreatedDate")) %></p>
                                    </div>
                                    <div class="card-footer d-flex justify-content-between bg-light border">
                                        <a href="ShopDetail.aspx?ProductId=<%# Eval("ProductId") %>" class="btn btn-sm text-dark p-0"><i class="fas fa-eye text-primary mr-1"></i>View Detail</a>
                                        <a href="ShopDetail.aspx?ProductId=<%# Eval("ProductId") %>" class="btn btn-sm text-dark p-0"><i class="fas fa-shopping-cart text-primary mr-1"></i>Add To Cart</a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


                    <div class="col-12 pb-1">
                        <nav aria-label="Page navigation">
                            <ul class="pagination justify-content-center mb-3">
                                <asp:Repeater ID="rptPager" runat="server" OnItemCommand="rptPager_ItemCommand">
                                    <ItemTemplate>
                                        <li class='<%# GetPagerCssClass(Convert.ToInt32(Eval("PageIndex"))) %>'>

                                            <asp:LinkButton ID="btnPage" CssClass="page-link" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Page">
                                                    <%# Eval("PageIndex") %>
                                            </asp:LinkButton>

                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ul>
                        </nav>
                    </div>


                </div>
            </div>
            <!-- Shop Product End -->
        </div>
    </div>
    <!-- Shop End -->

    <script type="text/javascript">
        function validateNumberInput(event) {
            var key = window.event ? event.keyCode : event.which;

            if (event.keyCode === 8 || event.keyCode === 46 || event.keyCode === 37 || event.keyCode === 39) {
                return true;
            } else if (key < 48 || key > 57) {
                return false;
            } else return true;
        }
    </script>


</asp:Content>
