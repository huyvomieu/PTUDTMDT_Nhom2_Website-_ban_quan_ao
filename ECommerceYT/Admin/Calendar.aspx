<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="ECommerceYT.Admin.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .calendar {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            grid-template-rows: auto;
            gap: 5px;
        }
        .calendar .day {
            position: relative;
            text-align: center;
        }
        .calendar .day-number {
            font-size: 16px;
            margin-bottom: 5px;
        }
        .calendar .order-count {
            position: absolute;
            top: -10px;
            right: -10px;
            width: 20px;
            height: 20px;
            border-radius: 50%;
            background-color: #007bff;
            color: white;
            font-size: 12px;
            line-height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="mt-4 mb-4">Order Calendar</h2>
            <div class="calendar">
                <%-- Loop through each month --%>
                <% for (int month = 1; month <= 12; month++) { %>
                    <%-- Display month header --%>
                    <div class="text-center mb-4">
                        <h4><%= System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) %></h4>
                    </div>
                    <%-- Display calendar days --%>
                    <div class="row">
                        <% for (int day = 1; day <= DateTime.DaysInMonth(DateTime.Now.Year, month); day++) { %>
                            <%-- Get the order count for the day --%>
                            <% int orderCount = GetOrderCountForDay(new DateTime(DateTime.Now.Year, month, day)); %>
                            <%-- Display day --%>
                            <div class="col day">
                                <div class="day-number"><%= day %></div>
                                <%-- Display order count only if greater than 0 --%>
                                <% if (orderCount > 0) { %>
                                    <div class="order-count"><%= orderCount %></div>
                                <% } %>
                            </div>
                        <% } %>
                    </div>
                <% } %>
            </div>
        </div>
    </div>
</div>

</asp:Content>
