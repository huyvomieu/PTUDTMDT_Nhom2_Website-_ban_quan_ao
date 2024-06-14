<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ECommerceYT.Admin.Product" %>


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


<%--    <script>
        window.onload = function () {
            var sizedropdown = document.getElementById('<%= ddlSize.ClientID %>');
            sizedropdown.onchange = function () {
                var selectedsize = sizedropdown.value; // lấy giá trị của kích thước đã chọn
                // gán giá trị đã chọn vào trường hoặc biến khác, ví dụ productsize
                document.getelementbyid('<%= hfProductSize.ClientID %>').value = selectedsize;
            };
        };
    </script>--%>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Product</h4>
                    <hr />

                    <div class="form-body">
                        <label>Product Name</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter ProdcutName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfProductName" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductName"
                                        ErrorMessage="Product Name is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Description</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfDescription" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDescription"
                                        ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Price</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfMobile" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice"
                                        ErrorMessage="Price is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Quantity</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Quantity"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfEmail" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity"
                                        ErrorMessage="Quantity is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


<%--                    <div class="form-body">
                        <label>Color</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" placeholder="Color"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfPostCode" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtColor"
                                        ErrorMessage="Color is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-body">
                        <label>Size</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="XL" Value="XL"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rvfSize" runat="server" ForeColor="Red" Font-Size="Small"
                                        Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlSize"
                                        ErrorMessage="Size is required"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hfProductSize" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>--%>


                      <div class="form-body">
                        <label>Color</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBoxList ID="cblColor" runat="server" >
                                        <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                        <asp:ListItem Text="Blue" Value="Blue"></asp:ListItem>
                                        <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                        <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-body">
                        <label>Size</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBoxList ID="cblSize" runat="server" >
                                        <asp:ListItem Text="XS" Value="XS"></asp:ListItem>
                                        <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="XL" Value="XL"></asp:ListItem>
                                    </asp:CheckBoxList>
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

                    <%--                    <asp:HiddenField ID="hfSold" Value="0" runat="server" />--%>

                    <div class="form-body">
                        <label>Product Image</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this);" />
                                    <asp:HiddenField ID="hfProductId" Value="0" runat="server" />
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
        
        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Product List</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rCategory_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Name</th>
                                            <th>Description</th>
                                            <th>Price</th>
                                            <th>Quantity</th>
                                            <th>Size</th>
                                            <th>Color</th>
                                            <th>CategoryId</th>
                                            <th>Sold</th>
                                            <th>IsActive</th>
                                            <th>Image</th>
                                            <th>CreatedDate</th>
                                            <th class="datatabe-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("ProductName") %> </td>
                                    <td><%# Eval("Description") %></td>
                                    <td><%# Eval("Price") %></td>
                                    <td><%# Eval("Quantity") %></td>
                                    <td><%# Eval("Size") %></td>
                                    <td><%# Eval("Color") %></td>
                                    <td><%# Eval("CategoryId") %></td>
                                    <td><%# Eval("Sold") %></td>
                                    <td>
                                        <asp:Label ID="lblIsActive" runat="server"
                                            Text='<%# ECommerceYT.Utils.GetIsActiveText(Eval("IsActive")) %>'
                                            CssClass='<%# ECommerceYT.Utils.GetIsActiveCssClass(Eval("IsActive")) %>'>
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <img width="40" src='<%# ECommerceYT.Utils.GetImageUrl(Eval("ImageUrl")) %>' />
                                    </td>

                                    <td><%# Eval("CreatedDate") %></td>
                                    <td>
                                        <asp:LinkButton CommandName="edit" runat="server" ID="lbEdit" Text="Edit" CssClass="badge badge-primary" CommandArgument='<%#Eval("ProductId") %>' CausesValidation="false">
                                    <i class="fas fa-edit"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton CommandName="delete" runat="server" ID="lbDelete" Text="Delete" CssClass="badge badge-danger" CommandArgument='<%#Eval("ProductId") %>' CausesValidation="false">
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
