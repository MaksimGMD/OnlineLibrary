<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="BooksInProgressPage.aspx.cs" Inherits="LibraryOnline.Pages.Users.BooksInProgressPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsBooksInProgress" runat="server"></asp:SqlDataSource>
    <div class="wrapper">
        <div class="container mt-4 mb-5">
            <nav class="nav mb-3">
                <a class="nav-link" href="UserPage.aspx"><i class="far fa-address-card"></i>Личные данные</a>
                <a class="nav-link" href="Order.aspx"><i class="fas fa-bookmark"></i>Заказы</a>
                <a class="nav-link" href="BooksInProgressPage.aspx" style="border-bottom: 3px solid #007bff;">
                    <i class="fas fa-thumbtack"></i>Активные книги</a>
                <a class="nav-link" href="HistoryPage.aspx"><i class="fas fa-history"></i>История</a>
            </nav>
            <center>
            <div class="OrderTable mt-3">
                <asp:GridView ID="gvBooks" runat="server" UseAccessibleHeader = "true" CssClass="table table-condensed table-hover"
                    CurrentSortDirection="ASC" AllowSorting="true" CellPadding="5" OnSorting="gvBooks_Sorting">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
