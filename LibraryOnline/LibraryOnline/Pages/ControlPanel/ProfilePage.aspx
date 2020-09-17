<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="LibraryOnline.Pages.ControlPanel.ProfilePage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <script type="text/javascript">
        function isDelete() {
            return confirm("Вы уверенны, что хотите удалить выбранную строку?");
        }
    </script>
    <%--Маска для ввода номера телефона--%>
    <script>
        $(document).ready(function () {
            $('#<%=tbPhone.ClientID%>').mask("+7 999 999-9999");

        });
    </script>
    <asp:SqlDataSource ID="sdsProfile" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPosition" runat="server"></asp:SqlDataSource>
    <div class="row mb-4"></div>
    <div class="container" id="Content">
        <center>
            <h2>Анкеты</h2>
        </center>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbName" runat="server" CssClass="tbForm" placeholder="Имя" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите имя" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbSurname" runat="server" CssClass="tbForm" placeholder="Фамилия" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите фамилию" ControlToValidate="tbSurname" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbAge" runat="server" CssClass="tbForm" placeholder="Возраст" MaxLength="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите возраст" ControlToValidate="tbAge" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:Label ID="lblAgeCheck" runat="server" Visible="false" CssClass="Error" Text="Возраст должен быть больше или равен 18" Display="Dynamic"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 mt-2">
                <asp:TextBox ID="tbPhone" runat="server" CssClass="tbForm" placeholder="Номер телефона" type="tel"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер телефона"
                    Display="Dynamic" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="DropDownList"></asp:DropDownList>
            </div>
            <div class="col-lg-4 mt-2">
                <asp:CheckBox ID="cbEducation" runat="server" />
                <label style="font-size: 16px; font-weight: 400">Высшее образование</label>
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
            <button id="btCanselSelected" runat="server" onserverclick="btCanselSelected_Click"
                style="background: none; padding: 0; margin-right: 5px; border: none">
                <i class="fas fa-times" title="Отменить выбор строки"></i>
            </button>
            <asp:Label ID="lblSelectedRow" runat="server" Style="font-weight: 500"></asp:Label>
        </div>
        <div class="table" style="overflow-x: auto; width: 100%">
            <asp:GridView ID="gvProfile" runat="server" AllowSorting="true"
                CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnSorting="gvProfile_Sorting" OnRowDataBound="gvProfile_RowDataBound" OnSelectedIndexChanged="gvProfile_SelectedIndexChanged" OnRowDeleting="gvProfile_RowDeleting">
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
