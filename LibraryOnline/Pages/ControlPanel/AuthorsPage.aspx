<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="AuthorsPage.aspx.cs" Inherits="LibraryOnline.Pages.ControlPanel.AuthorsPage" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <asp:SqlDataSource ID="sdsAuthors" runat="server"></asp:SqlDataSource>
    <div class="row mb-4">
        <nav class="nav">
            <a class="nav-link" href="BooksPage.aspx">Книги</a>
            <a class="nav-link" href="AuthorsPage.aspx">Авторы</a>
            <a class="nav-link" href="GenrePage.aspx">Жанры</a>
        </nav>
    </div>
    <div class="container">
        <center>
            <h2>Авторы</h2>
        </center>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbName" runat="server" CssClass="tbForm" placeholder="Имя" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  CssClass="Error" 
                    ErrorMessage="Введите имя" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbSurname" runat="server" CssClass="tbForm" placeholder="Фамилия" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  CssClass="Error" 
                    ErrorMessage="Введите фамилию" ControlToValidate="tbSurname" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbMiddleName" runat="server" CssClass="tbForm" placeholder="Отчество" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn-dark" ToolTip="Добавить новую запись" OnClick="btInsert_Click"/>
        <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" Visible="false" OnClick="btUpdate_Click"/>
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
            <button id="btCanselSelected" runat="server" onserverclick="btCanselSelected_Click"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fas fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%; text-align:center">
            <asp:GridView ID="gvAuthors" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnSorting="gvAuthors_Sorting" OnRowDataBound="gvAuthors_RowDataBound" OnSelectedIndexChanged="gvAuthors_SelectedIndexChanged" OnRowDeleting="gvAuthors_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-image.png" CausesValidation="False" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
