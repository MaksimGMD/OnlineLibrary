<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/ControlPanel/ControlPanel.Master" AutoEventWireup="true" CodeBehind="UsersPage.aspx.cs" Inherits="LibraryOnline.Pages.Admin.UsersPage" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
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
    <asp:SqlDataSource ID="sdsUsers" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsRole" runat="server"></asp:SqlDataSource>
    <div class="row mb-4"></div>
        <div class="container">
            <center>
            <h2>Пользователи</h2>
        </center>
            <div class="row">
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbName" runat="server" CssClass="tbForm" placeholder="Имя" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите имя" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbSurname" runat="server" CssClass="tbForm" placeholder="Фамилия" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите фамилию" Display="Dynamic" ControlToValidate="tbSurname"></asp:RequiredFieldValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbPhone" runat="server" CssClass="tbForm" placeholder="Номер телефона" type="tel"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер телефона" Display="Dynamic" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbPassportNumber" runat="server" CssClass="tbForm" placeholder="Номер паспорта" MaxLength="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите номер паспорта" Display="Dynamic" ControlToValidate="tbPassportNumber"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="Error" ErrorMessage="Номер паспорта должен содержать 4 цифры"
                        Display="Dynamic" ControlToValidate="tbPassportNumber" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbPassportSeries" runat="server" CssClass="tbForm" placeholder="Серия паспорта" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="Error" ErrorMessage="Введите серию паспорта" ControlToValidate="tbPassportSeries" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="Error" ErrorMessage="Серия паспорта должна содержать 6 цифр" Display="Dynamic"
                        ControlToValidate="tbPassportSeries" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="DropDownList"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbLogin" runat="server" CssClass="tbForm" placeholder="Логин" MaxLength="20"></asp:TextBox>
                    <asp:Label ID="lblLoginCheck" runat="server" CssClass="Error" Visible="false" Text="Пользователь с таким логином уже есть в базе" Display="Dynamic"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="Error" ErrorMessage="Введите логин" ControlToValidate="tbLogin" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="Error" ErrorMessage="Слишком короткий логин"
                        ControlToValidate="tbLogin" Display="Dynamic" ValidationExpression="\S{5,20}"></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:TextBox ID="tbPassword" runat="server" CssClass="tbForm" placeholder="Пароль" MaxLength="20"></asp:TextBox>
                    <br />
                    <asp:CheckBox ID="cbPasswordChange" runat="server" Text="Изменить пароль" Font-Size="14px" Visible="false" Display="Dynamic" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="Error" ErrorMessage="Введите пароль" ControlToValidate="tbPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="Error"
                        ErrorMessage="Длина пароля должна быть больше 6 символов, содеражть минимум одну ланитскую букву, одну цифру и один из символов !@#$%^&*_" ControlToValidate="tbPassword" Display="Dynamic"
                        ValidationExpression="(?=.*[0-9])(?=.*[!@#$%^&*_])((?=.*[a-z])|(?=.*[A-Z]))[0-9a-zA-Z!@#$%^&*_]{6,}"></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4 mt-2">
                    <asp:CheckBox ID="cbBan" runat="server" />
                    <label style="font-size: 16px; font-weight: 400">Заблокировать</label>
                </div>
            </div>
            <asp:Button ID="btInsert" runat="server" Text="Добавить" CssClass="btn-dark" OnClick="btInsert_Click" ToolTip="Добавить новую запись" />
            <asp:Button ID="btUpdate" runat="server" Text="Обновить" CssClass="btn-dark" ToolTip="Обновить запись" Style="margin-left: 1%" OnClick="btUpdate_Click" Visible="false" />
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
                <asp:GridView ID="gvUsers" runat="server" AllowSorting="true"
                    CssClass="table table-condensed table-hover" UseAccessibleHeader="true" CurrentSortDirection="ASC" Font-Size="14px" OnSorting="gvUsers_Sorting" AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" OnRowDeleting="gvUsers_RowDeleting">
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
