﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="LibraryOnline.Pages.Users.Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsOrder" runat="server"></asp:SqlDataSource>
    <div class="wrapper">
        <div class="container mt-4 mb-5">
            <nav class="nav mb-3">
                <a class="nav-link" href="UserPage.aspx"><i class="far fa-address-card"></i>Личные данные</a>
                <a class="nav-link" href="Order.aspx" style="border-bottom: 3px solid #007bff;"><i class="fas fa-bookmark"></i>Заказы</a>
                <a class="nav-link" href="BooksInProgressPage.aspx">
                    <i class="fas fa-thumbtack"></i>Активные книги</a>
                <a class="nav-link" href="HistoryPage.aspx"><i class="fas fa-history"></i>История</a>
            </nav>
            <center>
            <div class="OrderTable mt-3">
                <asp:GridView ID="gvOrders" runat="server" UseAccessibleHeader = "true" CssClass="table table-condensed table-hover"
                    CurrentSortDirection="ASC" AllowSorting="true" OnSorting="gvOrders_Sorting" OnRowDataBound="gvOrders_RowDataBound" OnRowDeleting="gvOrders_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Content/img/delete-image.png" ControlStyle-Width="24px" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>