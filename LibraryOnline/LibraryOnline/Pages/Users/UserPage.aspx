<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Users/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="LibraryOnline.Pages.UserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:SqlDataSource ID="sdsReadersInf" runat="server"></asp:SqlDataSource>
    <%--Маска для ввода номера телефона--%>
    <script>
        $(document).ready(function () {
            $('#<%=tbPhone.ClientID%>').mask("+7 999 999-9999");

        });
    </script>
    <div class="wrapper">
        <div class="container mt-4 mb-5">
            <nav class="nav mb-3">
                <a class="nav-link" href="UserPage.aspx" style="border-bottom: 3px solid #007bff;"><i class="far fa-address-card"></i>Личные данные</a>
                <a class="nav-link" href="Order.aspx"><i class="fas fa-bookmark"></i>Заказы</a>
                <a class="nav-link" href="BooksInProgressPage.aspx">
                    <i class="fas fa-thumbtack"></i>Активные книги</a>
                <a class="nav-link" href="HistoryPage.aspx"><i class="fas fa-history"></i>История</a>
            </nav>
            <div class="user-ticket">
                <div class="row">
                    <div class="data-block col">
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Логин</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbLogin" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblLoginCheck" runat="server" CssClass="Error" Visible="false" Text="Пользователь с таким логином уже есть в базе" Display="Dynamic"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="Error" ErrorMessage="Введите логин" ControlToValidate="tbLogin" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="Error" ErrorMessage="Слишком короткий логин"
                                    ControlToValidate="tbLogin" Display="Dynamic" ValidationExpression="\S{5,20}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Пароль</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbPassword" runat="server" CssClass="user-input" type="password" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="Error" ErrorMessage="Введите пароль" ControlToValidate="tbPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="Error"
                                    ErrorMessage="Длина пароля должна быть больше 6 символов, содеражть минимум одну ланитскую букву, одну цифру и один из символов !@#$%^&*_" ControlToValidate="tbPassword" Display="Dynamic"
                                    ValidationExpression="(?=.*[0-9])(?=.*[!@#$%^&*_])((?=.*[a-z])|(?=.*[A-Z]))[0-9a-zA-Z!@#$%^&*_]{6,}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Фамилия</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbSurname" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Error" ErrorMessage="Введите фамилию" Display="Dynamic" ControlToValidate="tbSurname"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Имя</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbName" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Error" ErrorMessage="Введите имя" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Номер телефона</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbPhone" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Error" ErrorMessage="Введите номер телефона" Display="Dynamic" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Номер паспорта</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbPassportNumber" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Error" ErrorMessage="Введите номер паспорта" Display="Dynamic" ControlToValidate="tbPassportNumber"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="Error" ErrorMessage="Номер паспорта должен содержать 4 цифры"
                                    Display="Dynamic" ControlToValidate="tbPassportNumber" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <strong>Серия паспорта</strong>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="tbPassportSeries" runat="server" CssClass="user-input" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="Error" ErrorMessage="Введите серию паспорта" ControlToValidate="tbPassportSeries" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="Error" ErrorMessage="Серия паспорта должна содержать 6 цифр" Display="Dynamic"
                                    ControlToValidate="tbPassportSeries" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col">
                                <asp:Button ID="btEdit" runat="server" Text="Редактировать" CssClass="user-btn" ToolTip="Редактировать личные данные" OnClick="btEdit_Click" CausesValidation="False" />
                                <asp:Button ID="btSave" runat="server" Text="Сохранить" CssClass="user-btn" ToolTip="Сохранить изменения" Visible="false" OnClick="btSave_Click" />
                                <asp:Button ID="btCansel" runat="server" Text="Отмена" Visible="false" CssClass="user-btn" ToolTip="Отменить редактирование" OnClick="btCansel_Click" CausesValidation="False" />
                                <asp:Button ID="btExit" runat="server" Text="Выйти" CssClass="user-btn" ToolTip="Выйти из учётной записи" CausesValidation="False" OnClick="btExit_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="card-block col">
                        <div class="reader-card">
                            <div class="reader-card-head">
                                <img src="../../Content/img/LibraryIcon.png" />
                                Библиотека
                            </div>
                            <div class="reader-card-body">
                                <h4>Билет читателя</h4>
                                <p>
                                    Билет №
                                    <asp:Label ID="lblTicketNumber" runat="server" Style="font-size: 20px; letter-spacing: 3px; background: #9CC6EE; padding: 3px">
                                    </asp:Label>
                                </p>
                                <asp:Label ID="lblTicketSurname" runat="server"></asp:Label>
                                <asp:Label ID="lblTicketName" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
