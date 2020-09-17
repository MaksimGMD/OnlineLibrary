<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="HistoryPage.aspx.cs" Inherits="LibraryOnline.Pages.HistoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsHistory" runat="server"></asp:SqlDataSource>
    <div class="wrapper">
        <div class="container mt-4 mb-5">
            <nav class="nav mb-3">
                <a class="nav-link" href="UserPage.aspx"><i class="far fa-address-card"></i>Личные данные</a>
                <a class="nav-link" href="Order.aspx"><i class="fas fa-bookmark"></i>Заказы</a>
                <a class="nav-link" href="BooksInProgressPage.aspx">
                    <i class="fas fa-thumbtack"></i>Активные книги</a>
                <a class="nav-link" href="HistoryPage.aspx" style="border-bottom: 3px solid #007bff;"><i class="fas fa-history"></i>История</a>
            </nav>
            <div class="row mb-3">
                <div class="col">
                    <div class="form-inline">
                        <asp:TextBox ID="tbSearch" CssClass="input-search" runat="server" placeholder="Поиск" type="text" style="width: 300px;">
                        </asp:TextBox>
                        <button runat="server" id="btSearch" class="btn-search" title="Поиск" onserverclick="btSearch_Click">
                            <i class="fa fa-search"></i>
                        </button>
                        <button id="btCansel" runat="server" class="btn-search" title="Отмена поиска" visible="false" onserverclick="btCansel_Click" style="border-radius: 10px; height: 40px; width: 40px; margin-left: 5px;"><i class="fas fa-times" style="font-size: 16px;"></i></button>
                    </div>
                </div>
            </div>
            <center>
            <div class="history-grid">
                <asp:GridView ID="gvHistory" runat="server" AllowSorting="true" OnSorting ="gvHistory_Sorting"
                    CssClass="table table-condensed table-hover" UseAccessibleHeader = "true" CurrentSortDirection="ASC">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
