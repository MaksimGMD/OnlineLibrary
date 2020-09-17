<%@ Page Title="Главная страница" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="LibraryOnline.Pages.MainPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
<%--    <link rel="stylesheet" href="../../Content/css/owl.carousel.css" />
    <link rel="stylesheet" href="../../Content/css/owl.theme.default.css" />--%>
    <link rel="stylesheet" href="../../Content/css/owl.carousel.css"/>
    <link rel="stylesheet" href="../../Content/css/owl.theme.default.css"/>
    <script src="../../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../../Scripts/owl.carousel.js"></script>
    <asp:SqlDataSource ID="sdsBookList" runat="server" ConnectionString="<%$ ConnectionStrings:LibraryConnectionString %>"
        SelectCommand="SELECT * FROM [Book_List] ORDER BY [ID], [Book_Name], [Year]"></asp:SqlDataSource>
    <div class="wrapper">
        <div class="container mt-4">
            <div class="row">
                <div class="owl-carousel owl-theme">
                    <asp:Repeater ID="rpCrouselItems" runat="server" DataSourceID="sdsBookList">
                        <ItemTemplate>
                            <div class="item">
                                <div class="card">
                                    <img src="<%#Eval("Image")%>" class="card-img-top" />
                                    <div class="card-body">
                                        <div class="card-description">
                                            <h2><%#Eval("Book_Name") %></h2>
                                            <h3><%#Eval("Author") %></h3>
                                        </div>
                                        <div class="card-btn">
                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Style="display:none" />
                                            <asp:Button class="btn btn-primary btn-sm" title="Добавить в список выбранных книг" runat="server" Text="Выбрать" OnClick="btInsertOrder_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <script src="../../Scripts/owl/owl.navigation.js"></script>
    <script src="../../Scripts/owl/owl.autoplay.js"></script>
    <script>
        $('.owl-carousel').owlCarousel({
            loop: true,
            margin: 50,
            nav: true,
            autoplay: true,
            autoHeight: true,
            autoplayTimeout: 5000,
            smartSpeed: 500,
            autoplayHoverPause: true,
            navText: ["<i class='fas fa-chevron-circle-left'></i>", "<i class='fas fa-chevron-circle-right'></i>"],
            responsive: {
                0: {
                    items: 1,
                    nav: false,
                    dots: false,
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 5
                }
            }
        })
    </script>
</asp:Content>
