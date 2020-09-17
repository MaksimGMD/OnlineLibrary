<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="BooksPage.aspx.cs" Inherits="LibraryOnline.Pages.ControlPanel.BooksPage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsBooks" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsAuthor" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsGenre" runat="server"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="row mb-4">
        <nav class="nav">
            <a class="nav-link" href="BooksPage.aspx">Книги</a>
            <a class="nav-link" href="AuthorsPage.aspx">Авторы</a>
            <a class="nav-link" href="GenrePage.aspx">Жанры</a>
        </nav>
    </div>
    <div class="container">
        <center>
            <h2>Книги</h2>
        </center>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbBookName" runat="server" CssClass="tbForm" placeholder="Название книги" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите название" ControlToValidate="tbBookName" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbYear" runat="server" CssClass="tbForm" placeholder="Год" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите год" ControlToValidate="tbYear" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbAmount" runat="server" CssClass="tbForm" placeholder="Количество" TextMode="Number" min="0"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Введите количество" ControlToValidate="tbAmount" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:DropDownList ID="ddlGenre" CssClass="DropDownList" runat="server" ToolTip="Жанр"></asp:DropDownList>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:DropDownList ID="ddlAuthor" CssClass="DropDownList" runat="server" ToolTip="Автор"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:FileUpload ID="flBookImage" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Выберите фотографию" ControlToValidate="flBookImage" CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Досутпны только файлы с расширением JPG, PNG"
                    ControlToValidate="flBookImage" ValidationExpression=".*(\w|\s|-])*\.(?:jpg|png)" CssClass="Error" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
        </div>
        <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click" />
        <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click" />
    </div>
    <div class="grid-system mt-5">
        <div class="row mb-2">
            <div class="col">
                <div class="form-inline">
                    <asp:TextBox ID="tbSearch" CssClass="input-search" runat="server" placeholder="Поиск" type="text" TextMode="Search"></asp:TextBox>
                    <button runat="server" id="btSearch" class="btn-search" title="Поиск" causesvalidation="False" onserverclick="btSearch_Click">
                        <i class="fa fa-search"></i>
                    </button>
                    <button runat="server" id="btFilter" class="btn-dark" onserverclick="btFilter_Click"
                        title="Фильтр" causesvalidation="False" style="width: 40px!important; height: 36px!important; margin: 0 0 0 1%; color: #e1f1ff;">
                        <i class="fas fa-filter"></i>
                    </button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="btn-dark" Visible="false"
                        ToolTip="Отменить поиск и фильтрацию" CausesValidation="False" Style="margin: 0 0 0 1%" OnClick="btCancel_Click" />
                </div>
            </div>
        </div>
        <div id="SelectedMessage" class="form-inline" runat="server" visible="false" display="Dynamic">
            <button id="btCanselSelected" runat="server" onserverclick="btCanselSelected_Click" causesvalidation="False"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fas fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%">
            <asp:GridView ID="gvBooks" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" OnSorting="gvBooks_Sorting" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvBooks_RowDataBound" OnSelectedIndexChanged="gvBooks_SelectedIndexChanged" OnRowDeleting="gvBooks_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" CausesValidation="False" ImageUrl="~/Content/img/delete-image.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
