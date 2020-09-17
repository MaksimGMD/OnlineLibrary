<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="BooksList.aspx.cs" Inherits="LibraryOnline.Pages.BooksList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsBookList" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAuthor" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsGenre" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsYear" runat="server"></asp:SqlDataSource>
    <div class="wrapper">
        <div class="container mt-4 mb-4">
            <h1>Список книг</h1>
            <div class="row mb-3">
                <div class="col">
                    <div class="form-inline">
                        <asp:TextBox ID="tbSearch" CssClass="input-search" runat="server" placeholder="Найти книгу" type="text">
                        </asp:TextBox>
                        <button runat="server" id="btSearch" class="btn-search" onserverclick="btSearch_Click" title="Поиск">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 mt-1">
                    <asp:DropDownList ID="ddlAuthor" runat="server" CssClass="filter-list" ToolTip="Фильтр по авторам">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-3 mt-1">
                    <asp:DropDownList ID="ddlGenre" runat="server" CssClass="filter-list" ToolTip="Фильтр по жанрам"></asp:DropDownList>
                </div>
                <div class="col-lg-1 mt-1">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="filter-list" ToolTip="Фильтр по годам"></asp:DropDownList>
                </div>


                <div class="col-lg-3 mt-1">
                    <asp:Button ID="btFilter" runat="server" CssClass="btn-filter" Text="Применить фильтр" OnClick="btFilter_Click" ToolTip="Применить фильтр" />
                    <button runat="server" id="btCansel" class="btn-cancel" onserverclick="btCancel_Click" title="Отменить фильтрацию">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="row mt-3">
                <%--Выводит список книг--%>
                <asp:Repeater ID="rpBooksList" runat="server">
                    <ItemTemplate>
                        <div class="card col-lg-3 mb-4" style="width: 18rem;">
                            <div class="card-img-top">
                                <div class="book-amount" title="Количество доступных книг"><%#Eval("Quantity") %> шт.</div>
                                <img class="book-picture" src="<%#Eval("Image")%>" alt="Card image cap">
                            </div>
                            <div class="card-body">
                                <div class="card-description">
                                    <h2><%#Eval("Book_Name") %></h2>
                                    <h3><%#Eval("Author") %></h3>
                                    <p><%#Eval("Genre_Name") %></p>
                                </div>
                                <div class="card-btn">
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Style="display: none" />
                                    <asp:Button ID="btInsertOrder" class="btn btn-primary btn-sm"
                                        title="Добавить в список выбранных книг" runat="server" Text="Выбрать" OnClick="btInsertOrder_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
