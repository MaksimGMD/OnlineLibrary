<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="OrdersPage.aspx.cs" Inherits="LibraryOnline.Pages.ControlPanel.OrdersPage" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsOrders_List" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsBooks" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsTicketNumber" runat="server"></asp:SqlDataSource>
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <div class="row mb-4"></div>
    <div class="container">
        <center>
            <h2>Заказы</h2>
        </center>
        <div class="row">
            <div class="col-lg-8 mt-2">
                <asp:DropDownList ID="ddlBooks" CssClass="DropDownList" runat="server" ToolTip="Список книг"></asp:DropDownList>
                <asp:Label ID="lbBookAmount" runat="server" Text="Выбранной книги нет в наличии или закончились копии" Display="Dynamic" Visible="false" CssClass="Error"></asp:Label>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:DropDownList ID="ddlReaders" CssClass="DropDownList" runat="server" ToolTip="Номер билета"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:DropDownList ID="ddlStatus" CssClass="DropDownList" runat="server" ToolTip="Статус заказа">
                    <asp:ListItem Value="1">Выдана</asp:ListItem>
                    <asp:ListItem Value="2">Возвращена</asp:ListItem>
                    <asp:ListItem Value="3">Ожидание</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbIssuance_Date" runat="server" CssClass="tbForm" TextMode="Date" ToolTip="Дата выдачи"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Выберите дату" ControlToValidate="tbIssuance_Date"
                    CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbReturn_Date" runat="server" CssClass="tbForm" TextMode="Date" ToolTip="Дата возврата"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Выберите дату" ControlToValidate="tbReturn_Date"
                    CssClass="Error" Display="Dynamic"></asp:RequiredFieldValidator>
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
            <asp:GridView ID="gvOrders" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" OnSorting="gvOrders_Sorting" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvOrders_RowDataBound" OnSelectedIndexChanged="gvOrders_SelectedIndexChanged" OnRowDeleting="gvOrders_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btDelete" runat="server" ImageUrl="~/Content/img/delete-image.png" ControlStyle-Width="24px" CommandName="Delete" OnClientClick="return isDelete()" ToolTip="Удалить" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
