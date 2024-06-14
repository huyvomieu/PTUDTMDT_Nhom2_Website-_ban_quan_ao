<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="HomeAdmin.aspx.cs" Inherits="ECommerceYT.Admin.HomeAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Top Selling Products</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rptTopSellingProducts" runat="server">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Product Id</th>
                                            <th>Sold</th>
                                            <th>Total Money Earned</th>
                                            <th>Sold This Week</th>
                                            <th>Sold This Month</th>
                                            <th>Sold This Year</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("ProductId") %> </td>
                                    <td><%# Eval("Sold") %></td>
                                    <td><%# Eval("TotalMoneyEarned") %></td>
                                    <td><%# Eval("SoldThisWeek") %></td>
                                    <td><%# Eval("SoldThisMonth") %></td>
                                    <td><%# Eval("SoldThisYear") %></td>
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

        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Top Selling Products</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rptBad" runat="server">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Product Id</th>
                                            <th>Sold</th>
                                            <th>Total Money Earned</th>
                                            <th>Sold This Week</th>
                                            <th>Sold This Month</th>
                                            <th>Sold This Year</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("ProductId") %> </td>
                                    <td><%# Eval("Sold") %></td>
                                    <td><%# Eval("TotalMoneyEarned") %></td>
                                    <td><%# Eval("SoldThisWeek") %></td>
                                    <td><%# Eval("SoldThisMonth") %></td>
                                    <td><%# Eval("SoldThisYear") %></td>
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

        <div class="col-sm-12 col-md-8">
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Order Statistics</h4>
            <hr />

            <h5>Daily Statistics for This Month</h5>
            <asp:Repeater ID="rptDailyStats" runat="server">
                <HeaderTemplate>
                    <table class="table data-table-export table-hover nowrap">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Total Orders</th>
                                <th>Total Revenue</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Date") %></td>
                        <td><%# Eval("TotalOrders") %></td>
                        <td><%# Eval("TotalRevenue", "{0:C}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <hr />

            <h5>Monthly Statistics for This Year</h5>
            <asp:Repeater ID="rptMonthlyStats" runat="server">
                <HeaderTemplate>
                    <table class="table data-table-export table-hover nowrap">
                        <thead>
                            <tr>
                                <th>Month</th>
                                <th>Total Orders</th>
                                <th>Total Revenue</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Month") %></td>
                        <td><%# Eval("TotalOrders") %></td>
                        <td><%# Eval("TotalRevenue", "{0:C}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <hr />

            <h5>Yearly Statistics</h5>
            <asp:Repeater ID="rptYearlyStats" runat="server">
                <HeaderTemplate>
                    <table class="table data-table-export table-hover nowrap">
                        <thead>
                            <tr>
                                <th>Year</th>
                                <th>Total Orders</th>
                                <th>Total Revenue</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Year") %></td>
                        <td><%# Eval("TotalOrders") %></td>
                        <td><%# Eval("TotalRevenue", "{0:C}") %></td>
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
